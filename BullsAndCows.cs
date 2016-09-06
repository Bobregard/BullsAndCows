#include <iostream>
#include <string>
#include <time.h>
#include <math.h>

using namespace std;

int generateNumber(int num, int numberOfDigits);

void main() {
	int number = 0, userChoice, bulls = 0, cows = 0, calves = 0;
	string userNumber;
	bool gameIsOver = false, guessed = false;

	srand(time(NULL));
	cout << "Welcome to \"Bulls, cows and calves\"\n";

	while(!gameIsOver) {
		do {
			cout << "Choose how many digits you want the number to be (between 3 and 6):" << endl;
			cin >> userChoice;
			//make sure the user enters the correct number between 3 and 6 and generate random number with distinct digits.
			switch (userChoice) {
			case 3:
				number = generateNumber(number, 3);
				break;
			case 4:
				number = generateNumber(number, 4);
				break;
			case 5:
				number = generateNumber(number, 5);
				break;
			case 6:
				number = generateNumber(number, 6);
				break;
			default:
				cout << "Wrong input.\n";
				break;
			}	
		} while (userChoice < 3 || userChoice > 6);
		//turn the computer's number to string to compare its digits with the digits of the user's number more easily.
		string numToStr = to_string(number);
		calves = numToStr.length();
		guessed = false;
		cout << "Try to guess it:\n";
		while (!guessed) {
			cin >> userNumber;
			//check if the user has entered a valid number of digits.
			while (userNumber.length() != userChoice) {
				cout << "Your input must be " << userChoice << " digits!\n";
				cin >> userNumber;
			}
			if (userNumber == numToStr) {
				guessed = true;
				cout << "You guessed the number!\nDo you want to play again(y/n)?\n";
				cin >> userNumber;
				while(true) {
					if (userNumber == "N" || userNumber == "n") {
						cout << "Bye!\n";
						gameIsOver = true;
						break;
					}
					else if (userNumber == "Y" || userNumber == "y"){
						gameIsOver = false;
						break;
					}
					else {
						cout << "Do you want to play again(y/n)?\n";
						cin >> userNumber;
					}
				}
			}
			else {
				//find number of bulls
				for (int i = 0; i < userChoice; i++) {
					if (userNumber[i] == numToStr[i]) {
						bulls++;
					}
				}
				//find number of cows
				for (int i = 0; i < userChoice; i++) {
					for (int j = 0; j < userChoice; j++) {
						if (userNumber[i] == numToStr[j]) {
							cows++;
						}
					}
				}
				cows -= bulls;
				//number of calves + print information about number of bulls, cows and calves.
				calves -= cows + bulls;
				cout << "Bulls: "<< bulls << endl << "Cows: "<< cows << endl << "Calves: " << calves << endl << endl << "Try again:\n";
				bulls = 0;
				cows = 0;
				calves = userChoice;
			}	
		}
	}	
}

int generateNumber(int num, int numberOfDigits) {
	bool distinctDigits = false;
	while (!distinctDigits) {
		//generate a random number with userChoice digits.
		num = pow(10, numberOfDigits - 1) + rand() % ((int)pow(10, numberOfDigits) - (int)pow(10, numberOfDigits - 1));
		//check if the random number is made of distinct digits.
		string numToStr = to_string(num);
		for (int i = 0; i < numberOfDigits - 1; i++) {
			for (int j = i + 1; j < numberOfDigits; j++) {
				if (numToStr[i] != numToStr[j]) {
					distinctDigits = true;
				}
				else {
					distinctDigits = false;
					break;
				}
			}
			if (distinctDigits == false) {
				break;
			}
		}
		num = atoi(numToStr.c_str());
	}	
	cout << "The computer has generated a random number with " << numberOfDigits << " digits.\n";
	return num;
}
