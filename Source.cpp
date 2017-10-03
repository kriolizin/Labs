#include <iostream>
#include <omp.h>
#include <ctime>

using namespace std;

void main() {
	cout << "Input N, x and k: ";
	int x, k;
	long n;

	cin >> n >> x >> k;

	double *arrayA, *arrayB, *arrayC;

	arrayA = new double[n];
	arrayB = new double[n];
	arrayC = new double[n];

	arrayA[0] = x;
	arrayB[0] = 1 / x;

	for (long i = 1; i < n; i++) {
		arrayA[i] = (sin(x*i) + pow(x, 2) / i);
		arrayB[i] = (arrayB[i - 1] + x) / i;
	}

	double time = clock();

	omp_set_num_threads(k);

#pragma omp parallel
	{
		int thread_num = omp_get_thread_num(), nStart, nFinish;

		nStart = thread_num * (int)((n / k) + 0.5);
		nFinish = (thread_num == (k - 1)) ? n : (thread_num + 1) * (int)((n / k) + 0.5);

		for (long i = nStart; i < nFinish; i++) {
			arrayC[i] = arrayA[i] - arrayB[n - i - 1];
			arrayB[i] = (arrayA[i] + arrayC[i]) / 2;
		}
	}

	time = (clock() - time) / CLK_TCK;

	cout << "\nCalc time: " << time << "\nA\t\tB\t\tC";

	for (long i = 0; i < n; i++) {
		cout.precision(3);
		cout << '\n' << arrayA[i] << "\t\t" << arrayB[i] << "\t\t" << arrayC[i];
	}

}