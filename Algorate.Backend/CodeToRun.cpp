#include <iostream>
#include <iomanip>
#include <chrono>
#include <ctime>
#include <thread>

// Extra includes

using namespace std;

string test() { return "test"; }

bool Validation() { return true;}

int main()
{
    float duration = 0;
	 

	
    for (int i = 0; i < 3; i++) {
        std::clock_t c_start = std::clock();
        string temp = test();
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
