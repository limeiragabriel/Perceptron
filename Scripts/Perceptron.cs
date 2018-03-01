using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perceptron : MonoBehaviour {
	// implementacao - calculo do peso
	//w = w + N * (d(k) - y) * x(k)
	public List<Amostra> amostras = new List<Amostra> (); // listas das amostras
	public List<float> saidas = new List<float> ();       // saida esperada para cada amostra
	public List<float> pesos = new List<float>();         // lista dos pesos

	int epocas;                 // maximo de iteracoes
	float taxa_aprendizado;     // aprendizado
	float limiar;               // limiar
	int num_amostras;           //quantidade de amostras
	int num_amostra;            // elementos por amostra

	public event System.Action OnFinishtreining;  // evento fim do treino
	public event System.Action EGato;             // evento classificou como gato
	public event System.Action ECao;              // evento classificou como gato

	public static Perceptron instance;            // instancia do neuronio

	// ============================================================================
	void Awake(){
		instance = this;
		ContruirAmostras ();
	}
	void Start(){                                    // iniciaizacao
		epocas = 1000;
		limiar = -1;
		taxa_aprendizado = 0.1f;
		num_amostras = amostras.Count;
		num_amostra = amostras [0].valores.Count;
	}
	// ============================================================================

	// inicializa a lista de dados de cada amostra com os valores normalizados
	// bem como as saidas esperadas
	void ContruirAmostras(){
		foreach (Amostra a in amostras) {
			a.Construir ();
			saidas.Add (a.saidaEsperada);
		}
	}

	// treinamento do neuronio
	public void Treinar(){

		ContruirAmostras ();

		foreach(Amostra amostra in amostras){
			amostra.valores.Insert (0, -1f);
		}
		// inicia os pesos com valores aleatórios
		pesos.Clear ();
		for (int i = 0; i < num_amostra; i++) {
			pesos.Add (Random.Range (0.0f, 1.0f));
		}
		// insere o limiar no vetor de pesos
		pesos.Insert (0, limiar);
		// inicializando o contador de epocas - iteracoes
		int num_epocas = 0;

		while (true) {

			bool erro = false;  // inicializa flag de erro

			float contador_de_erros = 0;

			for (int i = 0; i < num_amostras; i++) {

				float u = 0;    // contador da amostra

				for (int j = 0; j < num_amostra + 1; j++) {    // somatrorio dos valores com peso
					u += pesos [j] * amostras [i].valores [j];
				}

				// y e o resultado usando a funcao de ativacao
				float y = sinal (u);

				// verifica se a saida da rede e diferente da desejada
				if(y != saidas[i]){
					// caucula o erro
					float erro_aux = saidas [i] - y;

					// ajuste dos pesos para cada elemento da amostra
					for (int j = 0; j < num_amostra + 1; j++) {
						pesos [j] = pesos [j] + taxa_aprendizado * erro_aux * amostras [i].valores[j];
					}

					erro = true; // flag de erro
					contador_de_erros++;
				}
			}

			num_epocas++; // incremento do numero de iteracoes
			float percent = (contador_de_erros * 100) / num_amostras;
			Debug.Log ("Iteração: " + num_epocas + " | Erros: " + percent + "%");
			// criterio de parada pelo numero de iteracoes ou inexistencia de erro
			if(num_epocas > epocas || !erro){
				if(OnFinishtreining != null){
					OnFinishtreining ();      // gatilho para fim de treino
				}
				break;
			}
		}
	}

	// funcao de teste - parametro = amostra a ser classificada
	public void Testar(Amostra amostra){

		amostra.valores.Insert (0, -1);

		// usa o vetor de pesos da fase de treino
		float u = 0;
		for (int i = 0; i < num_amostra + 1; i++) {
			u += pesos [i] * amostra.valores [i];
		}
		// caucula a saida do neuronio
		float y = sinal (u);
		// verifica a qual classe pertence
		if (y == -1) {
			// A amostra pertence a classe Gato"
			if(EGato != null){
				EGato ();          // trigger
			}
		} else {
			// A amostra pertence a classe Cao
			if(ECao != null){
				ECao();            // trigger
			}
		}
	}

	// funcao de ativacao do neuronio
	// saida -1 = gato / saida 1 = cao
	float sinal(float u){
		//if (u >= 0) { return 1;} else { return -1;}
		return (u >= 0)? 1 : -1;
	}

}
