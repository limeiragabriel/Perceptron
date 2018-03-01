using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject trainingMenu;
	public GameObject testMenu;
	public GameObject okbtn;
	public GameObject popup;
	public GameObject testarBtn;

	public Image caoOuGato;
	public Sprite cao;
	public Sprite gato;

	public Text concluidoText;
	public Text popupMsg;
	public Text popupTitle;

	public InputField pesoInput;
	public InputField alturainput;
	public InputField dorsoinput;
	public InputField horasInput;

	void Start(){
		Perceptron.instance.OnFinishtreining += FinishTrainig;
		Perceptron.instance.EGato += EGato;
		Perceptron.instance.ECao += Ecao;

		testarBtn.SetActive (false);
	}

	public void Treinar(){
		mainMenu.SetActive (false);
		trainingMenu.SetActive (true);
		Perceptron.instance.Treinar ();
		FinishTrainig ();
	}

	public void Testar(){
		mainMenu.SetActive (false);
		testMenu.SetActive (true);
	}

	public void RodarAlgoritmo(){

		float peso = float.Parse(pesoInput.text);
		float altura = float.Parse(alturainput.text);
		float dorso = float.Parse (dorsoinput.text);
		float horas = float.Parse (horasInput.text);

		//Amostra a = new Amostra (peso,altura,dorso,horas);
		Amostra a = ScriptableObject.CreateInstance<Amostra> ();
		a.peso = peso;
		a.altura = altura;
		a.dorso = dorso;
		a.horasDormeDia = horas;
		a.Construir ();

		Perceptron.instance.Testar (a);
	}

	public void Cancelar(){
		mainMenu.SetActive (true);
		testMenu.SetActive (false);
	}

	public void OK(){
		okbtn.SetActive (false);
		concluidoText.text = "Aguarde";
		mainMenu.SetActive (true);
		trainingMenu.SetActive (false);

		testarBtn.SetActive (true);
	}

	void FinishTrainig(){
		okbtn.SetActive (true);
		concluidoText.text = "Treinamento Concluido";
	}

	void EGato(){
		testMenu.SetActive (false);
		popup.SetActive (true);
		popupTitle.text = "É GATO!";
		popupMsg.text = "PARECE QUE A AMOSTRA INFORMADA TRATA-SE DE UM FELINO!";
		caoOuGato.sprite = gato;
	}
	void Ecao(){
		testMenu.SetActive (false);
		popup.SetActive (true);
		popupTitle.text = "É CAO!";
		popupMsg.text = "PARECE QUE A AMOSTRA INFORMADA TRATA-SE DE UM CAO!";
		caoOuGato.sprite = cao;
	}

	public void BackToMenu(){
		popup.SetActive (false);
		mainMenu.SetActive (true);
	}
}
