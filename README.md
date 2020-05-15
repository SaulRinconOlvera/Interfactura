# Interfactura
Código para interfactura

Ejercicios
1.	(Obligatorio) Crear un proyecto en ASP NET MVC para:
I.	Mostrar una página de captura de alumnos:
    i.      Nombre
    ii.      Apellido Paterno
    iii.      Apellido Materno
    iv.      Fecha de Nacimiento
    v.      Genero
    vi.      Fecha de Ingreso
    vii.      Activo
    viii.      RFC
II.	Guardar los datos capturados en un archivo de texto en la ruta de la carpeta “\Alumnos”. El nombre del archivo será el RFC
 
2.	(Obligatorio) Realizar un Web Services REST que reciba un campo RFC (texto) y un campo CFDI(XML) y realice las siguientes validaciones sobre el campo CFDI:
I.	El campo RFC corresponda al atributo RFC del nodo cfdi:Emisor
II.	Que el atributo versión del nodo cfdi:Comprobante tenga el valor “3.3”
III.	Que la suma de los atributos importe de los nodos cfdi:concepto sea igual al atributo SubTotal del nodo cfdi:comprobante
 
