using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova Amostra", menuName = "Amostra")]
public class Amostra : ScriptableObject {

	public List<float> valores;

	public float peso;
	public float altura;
	public float dorso;
	public float horasDormeDia;

	public int saidaEsperada;

	// construtor
	public Amostra(float _peso, float _altura, float _dorso, float _horas){
		peso = _peso;
		altura = _altura;
		dorso = _dorso;
		horasDormeDia = _horas;

		Construir ();
	}

	// normaliza os dados mapeando para valores de 0 a 1
	float Normalizar(float value, float max){
		return value / max;
	}

	// inicializa a lista de dados normalizada
	public void Construir(){
		valores = new List<float>();
		valores.Add(Normalizar( peso , 100000));      // 100kg
		valores.Add(Normalizar( altura, 2000 ));      // 2m
		valores.Add(Normalizar ( dorso, 2000 ));      // 2m
		valores.Add(Normalizar ( horasDormeDia, 24)); // 24h
	}
}
