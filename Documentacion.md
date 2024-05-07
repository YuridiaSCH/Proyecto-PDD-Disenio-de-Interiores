![image](https://github.com/YuridiaSCH/Proyecto-PDD-Disenio-de-Interiores/assets/124212145/2901482b-d759-4a78-8918-db6649d403ed)
![image](https://github.com/YuridiaSCH/Proyecto-PDD-Disenio-de-Interiores/assets/124212145/3fd52f91-1b9f-4213-bfac-b792671083bb)

## 📌 Objetivo.
Implementar una aplicación  que permita a los usuarios diseñar y visualizar interiores de manera interactiva.

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
### 🗂️ Plataforma Unity Y Patrón Command.
- La estructura que brinda Unity es un poco intuitiva y semi-sencilla de asimilar en su uso, y brillante para obtener assets de muebles (Furnitures).

- El patrón Command es un patrón de diseño de comportamiento que permite encapsular solicitudes como objetos, lo que le permite parametrizar los clientes con operaciones, hacer colas de solicitudes, registrar solicitudes y soportar operaciones que pueden deshacerse.

- La razon para el uso del patron command dentro del sistema fue encapsula la acción de colocar un objeto en una posición determinada en función de los parámetros proporcionados, por lo que se utiliza un diccionario para almacenar los comandos asociados a teclas específicas para instanciar los muebles y cuando se presiona una tecla, se busca el comando correspondiente en el diccionario y se ejecuta.

### 💻 Lenguaje de programación c#.
***🖋️ Código*** donde se empleo el patrón de diseño.
```csharp
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstanciarObjeto : MonoBehaviour
{
    private Dictionary<int, ICommand> commands = new Dictionary<int, ICommand>();

    public GameObject[] objectsToPlace; // Lista de objetos 3D que se pueden colocar
    public Camera selectedCamera; // Cámara seleccionada en Unity

    private void Start()
    {
        // Asegurarse de que hay al menos 10 objetos en la lista objectsToPlace
        if (objectsToPlace.Length >= 10)
        {
            // Asignar comandos a las teclas numéricas del 0 al 9
            for (int i = 0; i < 10; i++)
            {
                commands.Add(i, new PlaceObjectCommand(objectsToPlace[i], selectedCamera));
            }
        }
        else
        {
            Debug.LogError("No hay suficientes objetos en objectsToPlace.");
        }
    }

    private void Update()
    {
        // Verificar la entrada del usuario para las teclas numéricas del 0 al 9
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                ExecuteCommand(i);
                break; // Salir del bucle una vez que se haya encontrado la tecla presionada
            }
        }
    }

    private void ExecuteCommand(int key)
    {
        if (commands.ContainsKey(key))
        {
            commands[key].Execute();
        }
    }

    public interface ICommand
    {
        void Execute();
    }

    public class PlaceObjectCommand : ICommand
    {
        private GameObject objectToPlace;
        private Camera selectedCamera;

        public PlaceObjectCommand(GameObject objectToPlace, Camera selectedCamera)
        {
            this.objectToPlace = objectToPlace;
            this.selectedCamera = selectedCamera;
        }

        public void Execute()
        {
            // Calcular la posición de instancia utilizando la posición y la dirección de la cámara
            Vector3 spawnPosition = selectedCamera.transform.position + selectedCamera.transform.forward * 2f; // Ejemplo: a dos unidades frente a la cámara
            Quaternion spawnRotation = selectedCamera.transform.rotation;

            // Instanciar el objeto en la posición y rotación calculadas
            GameObject newObject = GameObject.Instantiate(objectToPlace, spawnPosition, spawnRotation);

            // Agregar la clase base ObjetoArrastrable al objeto instanciado
            newObject.AddComponent<ObjetoArrastrable>().camara = selectedCamera;
        }
    }

    public class ObjetoArrastrable : MonoBehaviour
    {
        private bool isDragging = false;
        private Vector3 offset;
        public Camera camara; // Variable para asignar la cámara en Unity

        void Start()
        {
            // Si no se asigna una cámara en el editor, utilizar la cámara principal
            if (camara == null)
            {
                camara = Camera.main;
            }
        }

        void Update()
        {
            // Si se hace clic y el objeto no está siendo arrastrado, intenta iniciar el arrastre
            if (Input.GetMouseButtonDown(0) && !isDragging)
            {
                TryStartDragging();
            }

            // Si se está arrastrando el objeto, actualizar su posición basado en la posición de la cámara
            if (isDragging)
            {
                UpdateDrag();
                HandleRotation();
                HandleScaling();
            }
        }

        void TryStartDragging()
        {
            Ray ray = camara.ScreenPointToRay(Input.mousePosition); // Utilizar la cámara asignada
            RaycastHit hit;

            // Realizar un raycast desde la cámara hacia el mundo
            if (Physics.Raycast(ray, out hit))
            {
                // Si el raycast golpea este objeto, comenzar a arrastrarlo
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    offset = transform.position - camara.transform.position;
                }
            }
        }

        void UpdateDrag()
        {
            // Obtener la dirección de vista del jugador
            Vector3 viewDirection = camara.transform.forward;

            // Mover el objeto en la dirección de la vista del jugador
            transform.position = camara.transform.position + viewDirection * offset.magnitude;
        }

        void HandleRotation()
        {
            // Rotar el objeto si se presionan ciertas teclas mientras se arrastra
            float rotationSpeed = 50f; // Velocidad de rotación

            if (Input.GetKey(KeyCode.K))
            {
                transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.H))
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.U))
            {
                transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.J))
            {
                transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
            }
        }

        void HandleScaling()
        {
            // Escalar el objeto usando la rueda del del ratón
            float scaleSpeed = 0.1f; // Velocidad de escalado

            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (scrollWheel != 0)
            {
                Vector3 scale = transform.localScale;
                scale += Vector3.one * scrollWheel * scaleSpeed;
                transform.localScale = scale;
            }
        }

        void OnMouseUp()
        {
            // Cuando se deja de arrastrar
            isDragging = false;
        }
    }
```

## 💾 Conclusión.
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
