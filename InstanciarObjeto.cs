using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstanciarObjeto : MonoBehaviour
{
    private Dictionary<int, ICommand> commands = new Dictionary<int, ICommand>();

    public GameObject[] objectsToPlace; // Lista de objetos 3D que se pueden colocar
    public Camera selectedCamera; // C�mara seleccionada en Unity

    private void Start()
    {
        // Asegurarse de que hay al menos 10 objetos en la lista objectsToPlace
        if (objectsToPlace.Length >= 10)
        {
            // Asignar comandos a las teclas num�ricas del 0 al 9
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
        // Verificar la entrada del usuario para las teclas num�ricas del 0 al 9
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
            // Calcular la posici�n de instancia utilizando la posici�n y la direcci�n de la c�mara
            Vector3 spawnPosition = selectedCamera.transform.position + selectedCamera.transform.forward * 2f; 
            Quaternion spawnRotation = selectedCamera.transform.rotation;

            // Instanciar el objeto en la posici�n y rotaci�n calculadas
            GameObject newObject = GameObject.Instantiate(objectToPlace, spawnPosition, spawnRotation);

            // Agregar la clase base ObjetoArrastrable al objeto instanciado
            newObject.AddComponent<ObjetoArrastrable>().camara = selectedCamera;
        }
    }

    //clase para permitir hacer que cada objeto en la lista, tenga las propiedades de arrastre y demas
    public class ObjetoArrastrable : MonoBehaviour
    {
        private bool isDragging = false;
        private Vector3 offset;
        public Camera camara; // Variable para asignar la c�mara en Unity

        void Start()
        {
            // Si no se asigna una c�mara en el editor, utilizar la c�mara principal
            if (camara == null)
            {
                camara = Camera.main;
            }
        }

        void Update()
        {
            // Si se hace clic y el objeto no est� siendo arrastrado, intenta iniciar el arrastre
            if (Input.GetMouseButtonDown(0) && !isDragging)
            {
                TryStartDragging();
            }

            // Si se est� arrastrando el objeto, actualizar su posici�n basado en la posici�n de la c�mara
            if (isDragging)
            {
                UpdateDrag();
                HandleRotation();
                HandleScaling();
            }
        }

        void TryStartDragging()
        {
            Ray ray = camara.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

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
            // Obtener la direcci�n de vista del jugador
            Vector3 viewDirection = camara.transform.forward;

            // Mover el objeto en la direcci�n de la vista del jugador
            transform.position = camara.transform.position + viewDirection * offset.magnitude;
        }

        void HandleRotation()
        {
            // Rotar el objeto si se presionan ciertas teclas mientras se arrastra
            float rotationSpeed = 50f; // Velocidad de rotaci�n

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
            // Escalar el objeto usando la rueda de desplazamiento del rat�n
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
            // Cuando se suelta el bot�n del rat�n, dejar de arrastrar
            isDragging = false;
        }
    }

}
