#include <iostream>
#include <conio.h>
using namespace std;

float num1 = 3.14159; //decimal pequeño
double num2 = 2.711828; //decimal grande

int num3 = 33; //entero modificable

const int num4 = 44; //entero constante, error si se intenta modificar

int main(){

//  num4 = 22; //error, no se puede modificar un entero constante

    num3 = 44;
    cout << "El numero es: " << num1<< endl;
    cout << "El numero es: " << num2<< endl;
    getch();
    return 0;
}