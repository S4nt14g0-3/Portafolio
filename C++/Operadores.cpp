#include <iostream>
#include <conio.h>
using namespace std;

int numero1 = 33;
int numero2 = 44;
int numero3 = 55;

int suma; // "+" variable que almacena el resultado de la suma
int resta; // "-" variable que almacena el resultado de la resta
int multiplicacion; // "*" variable que almacena el resultado de la multiplicacion
double division; // "/" variable que almacena el resultado de la division
double sobra; // "%" variable que almacena el residuo de la division
// a todos estos se les puede dar un asignador "+=", "-=", "*=", "/=", "%=" para modificar su valor 

int main(){
    suma = numero1 + numero2;
    resta = numero1 - numero2;
    multiplicacion = numero1 * numero3;
    division = (double)numero1 / numero2;
    sobra = (double)(numero3 % numero2);

    cout << "La suma es: " << suma << "\n\n"<< endl;
    cout << "La resta es: " << resta << "\n\n"<< endl;
    cout << "La multiplicacion es: " << multiplicacion << "\n\n"<< endl;
    cout << "La division es: " << division << "\n\n"<< endl;
    cout << "El residuo es: " << sobra << "\n\n"<< endl;

    getch();
    return 0;
}