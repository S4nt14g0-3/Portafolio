#include <iostream>
#include <conio.h>
using namespace std;


bool comprobador; //funciona como un boolean, puede ser true o false 
// 0 = false, 1 = true
// "!=" compara si dos valores son diferentes, devuelve true si lo son y false si no lo son

int numero1 = 33;
int numero2 = 44;



int main(){
    

    //se separan para usar la misma variable de comprobacion y no hayan errores de compilacion
    //si se usan seguidas las comprobaciones solo tomara la ultima ejecutada y no se podra ver el resultado de las anteriores
    comprobador = numero1 > numero2;
    cout << "El resultado de la comparacion es: " << comprobador << "\n\n"<< endl;

    comprobador = numero1 != numero2;
    cout << "El resultado de la comparacion es: " << comprobador << "\n\n"<< endl;
    
    
    
    
    getch();
    return 0;
}