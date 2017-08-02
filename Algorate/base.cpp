#include <iostream>
#include <iomanip>
#include <chrono>
#include <ctime>
#include <thread>

// Extra includes

using namespace std;

// Code

// Validation

int main()
{
    float duration = 0;
	// Input 

	// Output
    for (int i = 0; i < 3; i++) {
        std::clock_t c_start = std::clock();
        // FunctionCall
        std::clock_t c_end = std::clock();
        duration += 1000.0 * (c_end-c_start) / CLOCKS_PER_SEC;
    }
    duration = duration / 3;
	if (Validation()) {
	    cout << duration << " " << "true" << endl;
	} else {
	    cout << duration << " " << "false" << endl;
	}
    return 0;
}
