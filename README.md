![image](https://github.com/YuridiaSCH/Proyecto-PDD-Disenio-de-Interiores/assets/124212145/2901482b-d759-4a78-8918-db6649d403ed)
![image](https://github.com/YuridiaSCH/Proyecto-PDD-Disenio-de-Interiores/assets/124212145/3fd52f91-1b9f-4213-bfac-b792671083bb)

![image](https://github.com/YuridiaSCH/Proyecto-PDD-Disenio-de-Interiores/assets/124212145/2ad9b343-60b6-41b4-8480-2031a44175dd)

## 📌 Objetivo.
Implementar una aplicación  que permita a los usuarios diseñar y visualizar interiores de manera interactiva.

![image](https://github.com/YuridiaSCH/Proyecto-PDD-Disenio-de-Interiores/assets/124212145/e031b80b-d24b-4477-acab-d96c00d61911)

## ✅ Aspectos positivos.
- ***📍 Funcional.*** El sistema realizado permite realizar las opciones de; instanciar objetos, y a los mismos rotarlos, cambiar su tamaño y cambiar su posición. 
- ***🏹 Cumple el objetivo.*** El usuario es capaz de interactuar con los objetos generados, con la capacidad de diseñar y visualizar interiores de manera "interactiva".
- ***🔩 Patrones.*** Comprensión del patrón empleado, y a si mismo la asimilación de otros no implementados directamente.
- ***⚡ Sencillez.*** Al momento de utilizarlo es un poco sencillo de comprender un poco el modo de uso, ya que no tiene tantos ajustes o bien atajos complicados, todo esta en un mismo lugar. 

## ❌ Aspectos a mejorar.
- ***🏗️ Diseño.*** La interfaz es un poco abrupta debido al tiempo, el menú con los muebles a escoger pudio haber sido estructurado de forma diferente.
- ***👥 Apoyo al usuario.*** Faltó color e imágenes de apoyo con respecto a los muebles, de manera que el usuario pueda navegar y utilizar la aplicación eficiente.
- ***🛠️ Movimiento.*** Al manipular los muebles en ciertos casos llega a ser un poco incomodo, de modo que el arrastrar dichos objetos pudo desarrollarse de otro modo.
- ***⌨️ Limitaciones de uso.*** La aplicación estuvo planeada para utilizar tenologias inmersivas, sin embargo debido al poco tiempo, no se puedo llevar acabo de dicha forma, sin embargo sigue siendo funcional para diseñar interiores.

## ⚙️ Herramientas utilizadas.

![image](https://github.com/YuridiaSCH/Proyecto-PDD-Disenio-de-Interiores/assets/124212145/1b37fd1c-462c-465f-bf1c-0838549b83b6)

### 🗂️ Plataforma Unity Y Patrón Command.
- La estructura que brinda Unity es un poco intuitiva y semi-sencilla de asimilar en su uso, y brillante para obtener assets de muebles (Furnitures).

- El patrón Command es un patrón de diseño de comportamiento que permite encapsular solicitudes como objetos, lo que le permite parametrizar los clientes con operaciones, hacer colas de solicitudes, registrar solicitudes y soportar operaciones que pueden deshacerse.

- La razon para el uso del patron command dentro del sistema fue encapsula la acción de colocar un objeto en una posición determinada en función de los parámetros proporcionados, por lo que se utiliza un diccionario para almacenar los comandos asociados a teclas específicas para instanciar los muebles y cuando se presiona una tecla, se busca el comando correspondiente en el diccionario y se ejecuta.

### 💻 Lenguaje de programación c#.

![image](https://github.com/YuridiaSCH/Proyecto-PDD-Disenio-de-Interiores/assets/124212145/5021bb20-b763-43a2-95c9-8bd081cfc052)

Al utilizar la plataforma de Unity, se tiene que trabajar con c#, un lenguaje sencillo y práctico para implementar patrones de diseño. 

## 💾 Conclusión.

![image](https://github.com/YuridiaSCH/Proyecto-PDD-Disenio-de-Interiores/assets/124212145/beea0361-ec95-4903-ae20-c68edcd42a82)

Se comprendió el uso de diferentes tipos de patrones, ya sea de manera directa e indirecta, de tal modo que estas herramientas pueden ahorrar mucho trabajo sin alterar código, permitiendo el uso factible de estos y dandole una estructura organizada y simple al proyecto en ciertas áreas del código.

Se tiene como ejemplo de usos de patrones inderectos tales como:

- ***📎 Singleton.***
Aunque no se implementa explícitamente, se puede ver un comportamiento similar al patrón Singleton en la forma en que se maneja la cámara. Cuando no se asigna una cámara en el editor, se utiliza la cámara principal. Esto asegura que siempre haya una instancia de cámara disponible para ser utilizada.

- ***#️⃣ Iterador.***
El bucle for en el método Start() y Update() se puede considerar un ejemplo de iteración sobre una colección de objetos (en este caso, los comandos). También se puede ver una iteración similar en la verificación de la entrada del usuario en el método Update().

- ***👓 Observador.***
Hay una forma de observador presente en la relación entre la clase --InstanciarObjeto-- y las teclas presionadas por el usuario. La clase --InstanciarObjeto-- "observa" las entradas del usuario y toma medidas en función de esas entradas.

- ***📋 Strategy*** 
El uso de la interfaz --ICommand-- permite definir múltiples estrategias para ejecutar comandos, lo que proporciona flexibilidad en la elección de qué acción ejecutar en tiempo de ejecución.

Sin querer queriendo se implementaron demás patrones, aunque sea de forma no directa, pero al menos queda la interpretación.

# 👩‍💻 Datos del Alumno.
- ***⚡ Nombre:** Cortes Hernandez Yuridia Saray*
- ***#️⃣ Numero de Control:** 20210554*
- ***💻 Carrera:** Ing. En Sistemas Computacionales*
- ***📚 Materia:** Patrones de Diseño de Software*
- ***📄 Semestre:** 9no*
