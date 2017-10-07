#include <iostream>
#include <string>
#include <list>
#include <vector>
#include <windows.h>

using namespace std;

enum Lang { Rus, Eng };

vector<string> alphEng = {
	{ "abcde" },
	{ "fghik" },
	{ "lmnop" },
	{ "qrstu" },
	{ "vwxyz" } };

vector<string> alphRus = {
	{ "абвгде" },
	{ "жзиклм" },
	{ "нопрст" },
	{ "уфхцчш" },
	{ "щыьэюя" } };

vector<string> *alph;

bool find(int *dataSpace, char subj) {



	string str;
	vector<string>::iterator it = alph->begin();

	for (int i = 0; it < alph->end(); it++, i++) {

		for (int j = 0; j < (*it).length(); j++)
			if ((*it)[j] == subj) {
				dataSpace[0] = i;
				dataSpace[1] = j;

				return true;
			}
	}
	return false;
}

void BeepFunction(list<int> str) {

	list<int>::iterator it = str.begin();

	for (; it != str.end(); it++) {
		for (int j = 0; j < (*it); j++) {
			Beep(400, 100);
			Sleep(200);
		}

		Sleep(400);
	}

}

void EncryptString(string str, Lang lang) {

	alph = (lang == Eng ? &alphEng : &alphRus);

	string encryptedStr = "";
	list<int> encryptedNums;

	for (int i = 0; i < str.length(); i++) {
		int *encryptedNum = new int[2];
		if (!find(encryptedNum, str[i]))
			continue;

		encryptedNums.push_back(encryptedNum[0]+1);
		encryptedNums.push_back(encryptedNum[1]+1);

		if (encryptedNum[0] == (alph->size() - 1))
			encryptedNum[0] = 0;
		else
			encryptedNum[0] += 1;


		vector<string>::iterator it = alph->begin();
		for (int j = 0; j < encryptedNum[0]; j++, it++);

		encryptedStr += (*it)[encryptedNum[1]];
	}

	list<int>::iterator it = encryptedNums.begin();

	cout << "\nЗашифрованное цифрами сообщение: ";
	for (; it != encryptedNums.end(); it++) {
		cout << *it;
	}

	cout << "\nЗашифрованное буквами сообщение: ";
	cout << encryptedStr;

	cout << "\n";
	system("pause");
	BeepFunction(encryptedNums);


}

void DecryptString(string str, Lang lang) {

	alph = (lang == Eng ? &alphEng : &alphRus);

	string decryptedStr = "";

	for (int i = 0; i < str.length(); i++) {
		int *decryptedNum = new int[2];
		if (!find(decryptedNum, str[i]))
			continue;

		if (decryptedNum[0] == 0)
			decryptedNum[0] = alph->size() - 1;
		else
			decryptedNum[0] -= 1;

		vector<string>::iterator it = alph->begin();
		for (int j = 0; j < decryptedNum[0]; j++, it++);

		decryptedStr += (*it)[decryptedNum[1]];
	}

	cout << "\nРасшифрованное сообщение: ";
	cout << decryptedStr << '\n';
}

void main_menu() {
	cout << "Зашифровать.\n\n1: Русский язык.\n2: Английский язык.\n\nРасшифровать.\n\n3: Русский язык.\n4: Английский язык.\n\n >> ";
	int menuPointer = 0;
	cin >> menuPointer;

	Lang lang = (menuPointer % 2 == 1) ? Rus : Eng;



	cout << "\nВведите строку: ";
	string str;
	cin >> str;

	if (menuPointer <= 2)
		EncryptString(str, lang);
	else 
		DecryptString(str, lang);
	


}

void main() {
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);

	main_menu();
}
