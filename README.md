#StlBackEnd
# STOCKTACKING LOAD

### Problemas a resolver
Lo que se intenta lograr con este software es automatizar el registro de préstamo que es realizado a mano. Siendo este fácil de manipular y algo propenso a cometer errores. ​
Como objetivo se proyecta un ahorro significativo en este proceso, además de tener tus elementos o productos registrados para hacer más simple su búsqueda en la bodega. ​

### Desarrollo a la medida 
Este es un desarrollo a la medida porque el problema tiene una particularidad y es que los productos que existen en el mercado no encajan con el problema que queremos resolver.

### Modelo de Dominio anémico

![](https://cdn.discordapp.com/attachments/1010673900398587974/1039674395750301747/image.png)
Para el modelo de domino lo primero que tenemos es una empresa que es la entidad a la cual pertenece un inventario que contiene otros inventarios por áreas; un administrador que es quien maneja toda la información y permisos de la aplicación ya que es capaz de crear diferentes áreas y monitores. Adicional a esto puede realizar préstamos dentro de las diferentes áreas. Por lo tanto en una empresa puede haber uno o muchos administradores.  
Tenemos un inventario que es el contiene los diferentes productos, estos se pueden agregar, actualizar, consultar. Hay unos productos que son aquellos elementos que hay dentro del área para en nuestro caso ser prestado; seguido a esto tenemos un monitor que es aquella persona con menos permisos dentro del área pero que puede realizar préstamos, el préstamo que es la acción de brindarle un producto a otra persona por un determinado tiempo. Hay una observación que es la que lleva el detalle del préstamo a la hora de ser entregado y devuelto; por último pero no menos importante está el prestador que es aquella persona que solicita un préstamo de un producto.
En cuanto a las dependencias tenemos que en una empresa puede existir uno o muchos administradores, al igual que pueden existir una o muchas áreas. A cada área pertenecen uno o muchos monitores y tiene uno o muchos inventarios; los inventarios contiene uno o muchos productos, los cuales van a pertenecer a cero o muchos préstamos, estos préstamos se pueden realizar por un monitor quién puede crear cero o muchos préstamos. Este último s realizado por un prestador que puede solicitar cero o muchos préstamos, por último este préstamo tiene una muchas observaciones.
### Proposito del Diseño
Nuestro cliente es la UCO que necesita un mejor manejo de inventario de elementos electrónicos y préstamo el mismo,
STL es una aplicación SPA que agiliza el proceso de inserción de información y da una mejor busqueda en el inventario actual.
A diferencia de bionix y kimaldi nuestro producto facilita el identificar el personal y el cliente mediante RFID
### Event Storming
<https://miro.com/app/board/uXjVPXcey0g=/>
Tenemos como elemento principal e tener un administrador el cual se puede crear, modificar y consultar para que pueda a su vez crear la empresa la cual se podrá crear, consultar y modificar;  las áreas que son las siguientes en el flujo se pueden crear y consultar, luego tenemos a un monitor con las mismas funcionalidades que el administrador quien puede crear el inventario actualizarlo, activarlo, consultarlo; seguido a al inventario se encuentra el producto que se podrá crear, consultar, actualizar, eliminar, prestar u agotar. Luego tenemos el préstamo que es donde va la información de éste y que por sus características se podrá crear, actualizar, consultar, vencer (En este caso para cuando el prestador se pasa del límite de tiempo establecido para el préstamo) y cerrar (Esto es para cuando se devuelven todos los elementos prestados y se finaliza el préstamo), por último está la observación que se crea cuando se realiza la devolución de un producto o se va a extender el tiempo del préstamo entonces se deja una anotación.
### Tácticas y Estrategias 
Táctica: se requiere crear un log in para validar de forma correcta a los monitores y administradores, ya que estos son los encargados de darle inicio a la aplicación y tienen permisos especiales para modificar el inventario o dar de baja los elementos que este mismo posee.
Inicialmente se pensó en que soluciones podríamos tener si hacer nuestro propio log in o usar algún servicio externo que nos valide esto.
Realizando una investigación nos damos cuenta que es mejor la segunda opción recurrir a un servicio externo por temas de velocidad y seguridad.
### Funcionalidades Criticas
- Realizar un préstamo: Esta funcionalidad se tomo como critica por el hecho de que es fundamental para la aplicación, sino la app se convertiría en un gestor de inventario. Hace referencia a el proceso que se lleva a cabo para prestar un artículo.
- Log in: sin esta funcionalidad se corre el riesgo de que la aplicación utilice la información de toda la base de datos en vez de solo la entidad que la esta utilizando en el momento
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
|Base Datos      |MySql-Prostgresl-Mongo-Sql Lite |**MySQL**                |
|Proveedor Autenticación |Auth0-Oauth 2.0         |**Auth0**                   |
|Proveedor de Api Gateway| Api Gateway Google-WSO2-Api Management|**WSO2**     |
|Proveedor de CDN|MaxCDN-Cloudflare-Imperva| **Imperva**|
|Proveedor de service  mesh|AWS App Mesh-Network Service  Mesh (NSM)|**Network Service  Mesh (NSM)**|
|Proveedor de WAF|Cloudflare WAF-Prophaze WAF|**Cloudflare WAF**|

Para nuestro proyecto se presentaron estas alternativas de solución por presupuesto, conocimiento y porque según lo investigado de adecuaba bien al proyecto. En frontend seleccionamos ángular por su curva de aprendizaje además de que nos permite crear un build en el escritorio para usar la aplicación de forma offline , por otro lado para el backend decidimos usar C# .Net por conocimiento y tiene programación orientada objetos, adicionalmente tiene fácil manejo de datos, la base de datos MySQL  por la gran cantidad de datos que puede almacenar y teniendo en cuenta que es una aplicación para la universidad debemos tener buen espacio para los datos; las demás alternativas fueron seleccionadas porque son gratuitas y por presupuesta era la mejor solución
### Arquitectura de Referencia
Arquitectura por capas porque no requiere mucho tiempo de desarrollo y es fácil de mantener ya que las tareas se delegan por capas y por ello es fácil de testear.
### Arquetipo Base De La Aplicación
![](https://cdn.discordapp.com/attachments/1010673900398587974/1039685476342321213/image.png)
Tenemos un cliente con una base de datos Sql Lite para que funcione de manera offline el cual es una de las reglas del negocio. se necita dado que la situación en la UCO puede varias con respecto a ala ausencia de internet y aun así la app debe seguir funcionando.
api management nos permite gestionar la api y así saber la demanda de nuestros servicios para adaptaciones futuras.
El autenticador nos sirve para poder hacer una buena identificación a los administradores de nuestra aplicación.
las otras personas serán identificadas mediante nuestro sistema RfId
### Diagrama de Clases 
![](https://cdn.discordapp.com/attachments/1010673900398587974/1039707845748346960/ClassDiagram1.png)
El diagrama de clases presenta los mismos objetos que se dan en el modelo de dominio y llevan en su mayoría una relación de asociación porque tiene una relación entre ellos pero no es de dependencia, como por ejemplo la relación entre el préstamo y el prestador que es de dependencia puesto que para que se de un préstamo se necesita que haya un prestado. Por otro lado hay unas relaciones de composición porque se bien una necesita de la otra, no son tan necesarias, se componen entres si.
### Modelo Entidad Relación
![](https://cdn.discordapp.com/attachments/1010673900398587974/1039717978079514654/Relational_1.png)
Para el MER nos basamos en el diagrama de clases, sin embargo lo que eran monitor, administrador, y prestador se combinar y sólo quedó persona puesto que compartían los mismos atributos, aunque se creó una entidad de prestador para que llevara los datos adicionales que lleva éste.  
### Diagrama de Componentes BackEnd
![](https://cdn.discordapp.com/attachments/1039553534510776331/1039761174679535708/image.png) 
### Diagrama de Paquetes BackEnd
![](https://cdn.discordapp.com/attachments/974810763707420733/1039765592489594920/ProyectoExtraClase-DiagramaDePaquetes.drawio.png)

### Diagrama de Actividades 
![](https://cdn.discordapp.com/attachments/615334583864393780/1039722740111904818/image.png)
Para el registro de una persona en la aplicación es necesario que se valide que los datos ingresados sean válidos, si lo son se procede a validar que esta persona no exista aún, si lo hace se manda un mensaje donde se informa al usuario que dicha persona ya se encuentra en la base de datos; por otro lado si el usuario no existe, con los datos ingresados se registra la persona

### Diagrama de Estados 
![](https://cdn.discordapp.com/attachments/615334583864393780/1039722599128760390/image.png)
Este se utiliza para verificar que efectivamente al yo querer registrar una persona que ya esta no se me duplique y así mismo me valida que no ingrese datos erróneos ni repetidos en el sistema.
.
