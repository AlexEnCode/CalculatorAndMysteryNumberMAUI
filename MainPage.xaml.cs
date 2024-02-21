using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1
{
	public partial class MainPage : ContentPage, INotifyPropertyChanged
	{
		private int NumberToGuess;
		private int Guess;
		private int AttemptsLeft = 5;
		private bool GameEnded = false;
		private string ErrorMessage;
		private string Hearts = "❤❤❤❤❤";


		public MainPage()
		{
			InitializeComponent();
			GenerateNumberToGuess();
		}

		private void GenerateNumberToGuess()
		{
			Random random = new Random();
			NumberToGuess = random.Next(1, 21);
		}

		private void OnTestClicked(object sender, EventArgs e)
		{
			if (!GameEnded)
			{
				string input = numberEntry.Text;
				if (int.TryParse(input, out Guess))
				{
					CheckGuess();
					UpdateResultLabel();
				}
				else
				{
					ErrorMessage = "Veuillez entrer un nombre entier.";
					UpdateResultLabel();
				}
			}
		}



		private void CheckGuess()
		{
			Hearts = Hearts.Substring(0, Hearts.Length - 1);
			AttemptsLeft--;
			heartsLabel.Text = Hearts;

			if (Guess == NumberToGuess)
			{
				GameEnded = true;
				return;
			}

			if (AttemptsLeft == 0)
			{
				GameEnded = true;
				ErrorMessage = "Vous n'avez plus d'essais!";
				return;
			}

			if (Guess < 1 || Guess > 20)
			{
				ErrorMessage = "Veuillez entrer un nombre entre 1 et 20.";
				return;
			}

			if (Guess < NumberToGuess)
			{
				ErrorMessage = "Le nombre à deviner est plus grand.";
			}
			else
			{
				ErrorMessage = "Le nombre à deviner est plus petit.";
			}
		}

		private void UpdateResultLabel()
		{
			if (GameEnded)
			{
				if (Guess == NumberToGuess)
				{
					resultLabel.Text = "Félicitations! Vous avez deviné le nombre mystère!";
					resultLabel.TextColor = Color.FromHex("#ffffff");
				}
				else
				{
					resultLabel.Text = ErrorMessage;
					resultLabel.TextColor = Color.FromHex("#ffffff");
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(ErrorMessage))
				{
					resultLabel.Text = ErrorMessage;
					resultLabel.TextColor = Color.FromHex("#ffffff");
				}
				else
				{
					resultLabel.Text = $"Tentatives restantes: {AttemptsLeft} {Hearts}";
					resultLabel.TextColor = Color.FromHex("#ffffff");
				}
			}
		}


		private void OnResetClicked(object sender, EventArgs e)
		{
			AttemptsLeft = 5;
			GameEnded = false;
			ErrorMessage = "";
			Hearts = "❤❤❤❤❤";
			heartsLabel.Text = Hearts;
			resultLabel.Text = "";
			GenerateNumberToGuess();
		}
	}
}