# StlBackEnd
# STOCKTACKING LOAD

### Problemas a resolver
Lo que se intenta lograr con este software es automatizar el registro de préstamo que es realizado a mano. Siendo este fácil de manipular y algo propenso a cometer errores. ​
Como objetivo se proyecta un ahorro significativo en este proceso, además de tener tus elementos o productos registrados para hacer más simple su búsqueda en la bodega. ​

### Desarrollo a la medida 
Este es un desarrollo a la medida porque el problema tiene una particularidad y es que los productos que existen en el mercado no encajan con el problema que queremos resolver.

### Modelo de Dominio anémico

![](https://cdn.discordapp.com/attachments/1010673900398587974/1039674395750301747/image.png)
### Proposito del Diseño
Nuestro cliente es la UCO que necesita un mejor manejo de inventario de elementos electrónicos y préstamo el mismo,
STL es una aplicación SPA que agiliza el proceso de inserción de información y da una mejor busqueda en el inventario actual.
A diferencia de bionix y kimaldi nuestro producto facilita el identificar el personal y el cliente mediante RFID
### Event Storming
<https://miro.com/app/board/uXjVPXcey0g=/>
### Atributos de Calidad y escenarios
<https://cdn.discordapp.com/attachments/1010673900398587974/1039677572004462674/Taller_de_atributos_de_calidad1.xlsx>
### Tácticas y Estrategias 
Táctica: se requiere crear un log in para validar de forma correcta a los monitores y administradores, ya que estos son los encargados de darle inicio a la aplicación y tienen permisos especiales para modificar el inventario o dar de baja los elementos que este mismo posee.
Inicialmente se pensó en que soluciones podríamos tener si hacer nuestro propio log in o usar algún servicio externo que nos valide esto.
Realizando una investigación nos damos cuenta que es mejor la segunda opción recurrir a un servicio externo por temas de velocidad y seguridad.
### Funcionalidades Criticas
- Realizar un préstamo: Esta funcionalidad se tomo como critica por el hecho de que es fundamental para la aplicación, sino la app se convertiría en un gestor de inventario. Hace referencia a el proceso que se lleva a cabo para prestar un artículo.
- Log in: si esta funcionalidad se corre el riesgo de que la aplicación utilice la información de toda la base de datos en vez de solo la entidad que la esta utilizando en el momento
### Restricciones Técnicas
- Tener disponible la aplicación el 99.9% del tiempo, para que el administrador tenga la certeza de ingresar a revisar todo sin ningún problema cuando lo desee.
- Se debe tener como arquitectura de referencia la arquitectura por capas, ya que es fácil de implementar y rápida, además de ser fácil de mantener ya que se dividen las tareas en capas específicas, y también es más rápido probar ya que se puede realizar pruebas individuales para cada para cada capa.
- Hacer uso de integración continua para garantiza la calidad del código a la hora de hace despliegues
- Pruebas unitarias, integración continua.
### Alternativas de Solución
|                |opciones                          |Seleccionado              |
|----------------|--------------------------------|----------------------------|
|Front End       | Angular-React-Flutter          |**Angular**                 |
|Back End        |C# .Net Core-Spring boot- Django|**C# .Net Core**            |
|Base Datos      |MySql-Prostgresl-Mongo-Sql Lite |**Sql Lite**                |
|Proveedor Autenticación |Auth0-Oauth 2.0         |**Auth0**                   |
|Proveedor de Api Gateway| Api Gateway Google-WSO2-Api Management|**WSO2**     |
|Proveedor de CDN|MaxCDN-Cloudflare-Imperva| **Imperva**|
|Proveedor de service  mesh|AWS App Mesh-Network Service  Mesh (NSM)|**Network Service  Mesh (NSM)**|
|Proveedor de WAF|Cloudflare WAF-Prophaze WAF|**Cloudflare WAF**|


### Arquitectura de Referencia
Arquitectura por capas porque no requiere mucho tiempo de desarrollo y es fácil de mantener ya que las tareas se delegan por capas y por ello es fácil de testear.
### Arquetipo Base 
![](https://cdn.discordapp.com/attachments/1010673900398587974/1039685476342321213/image.png)
Tenemos un cliente con una base de datos Sql Lite para que funcione de manera offline el cual es una de las reglas del negocio. se necita dado que la situación en la UCO puede varias con respecto a ala ausencia de internet y aun así la app debe seguir funcionando.
api management nos permite gestionar la api y así saber la demanda de nuestros servicios para adaptaciones futuras.
El autenticador nos sirve para poder hacer una buena identificación a los administradores de nuestra aplicación.
las otras personas serán identificadas mediante nuestro sistema RfId
### Diagrama de Clases 
![](https://cdn.discordapp.com/attachments/1010673900398587974/1039707845748346960/ClassDiagram1.png)
### Modelo Entidad Relación
![](https://cdn.discordapp.com/attachments/1010673900398587974/1039717978079514654/Relational_1.png)
### Diagrama de Actividades 
![](https://cdn.discordapp.com/attachments/615334583864393780/1039722740111904818/image.png)
Para el registro de una persona en la aplicación es necesario que se valide que los datos ingresados sean válidos, si lo son se procede a validar que esta persona no exista aún, si lo hace se manda un mensaje donde se informa al usuario que dicha persona ya se encuentra en la base de datos; por otro lado si el usuario no existe, con los datos ingresados se registra la persona

### Diagrama de Estados 
![](https://cdn.discordapp.com/attachments/615334583864393780/1039722599128760390/image.png)
este se utiliza para verificar que efectivamente al yo querer registrar una persona que ya esta no se me duplique y así mismo me valida que no ingrese datos erróneos ni repetidos en el sistema.
.
