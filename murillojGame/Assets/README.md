# DAM 2. GAME UNITY
## DESARROLLADOR: JORGE MURILLO CARRERA

### Nombre del juego: *Banana & Plataforms*
### Estilo: Juego de plataformas hecho en 3D.

<hr>

### PARTES REALIZADAS POR MI:

Todo ha sido hecho por mi, todo el código de todos los scripts, y la propia implementación del personaje, enemigo, coleccionables, funcionalidades de los diferentes obstáculos y objetos, implementación de la música y efectos de sonido. 

También he desarrollado las pantallas de inicio y de fin completamente, además de los diferentes canvas que hay en el juego, como el de pausa, información de las bananas que se han recogido en el nivel, y botones de salida del juego y de mostrar el menu de pausa.

### PARTES COGIDAS DE INTERNET:

No he cogido ningún trozo de código, pero si los propios modelos 3D de los personajes y del mapa, así como he usado diferentes páginas web para las animaciones de estas, ya que venian sin ellas.

La página que he usado para animar, es **Mixamo**.

De las que he sacado el material 3D, son unicamente dos:

- Assert Store de Unity.

- Itch.io

Todo lo usado han sido modelos gratuitos subidos a estas plataformas.

### FUNCIONALIDADES A DESTACAR DEL JUEGO:

- El propio movimiento del personaje manteniendo el eje X  en 0, para que solo se pueda mover a lo largo del eje Z y saltando, también por el Y.

- La funcionalidad del salto y del doble salto, sacando unas alas unicamente si está ejecutandose el doble salto, y además controlar el buen funcionamiento de estos saltos sin poder llegar a aprovecharse de ellos.

- Fantasma, que se mueve por la zona indicada entre dos objetos vacios con un componente BoxCollider que actuan como límite de movimientos para los fantasmas. Cuentan con dos partes, la cabeza y el cuerpo, dependiendo de la colisión, hara daño al jugador o al fantasma.

- Aplicar una animación de muerte para el fantasma además de aplicar una fuerza positiva en el eje Y del personaje, después de haber puesto la velocidad a 0, cuando el fantasma es pisado por el jugador.

- Funcionalidad del menu de pausa.

- Puentes móviles, donde el personaje una vez lo esté tocando, se quede pegado a el, y pueda moverse libremente por este, y pararse en cualquier punto del puente mientras se mueve, junto con el personaje en estado idle.

- Tuberia, que genera bolas, que salen con una fuerza, y si golpean al jugador, le tiran del nivel. Además cabe destacar, que se controla la eliminación de las bolas creadas una vez se crea una nueva bola.

- Funcionalidad del seguimiento de la camara, de manera suave y con un poco de retardo, para dar mayor sensación de suavidad.

- Recolección de las bananas, además de la propia "vida" que le doy a estas, para que no sean coleccionables estáticos.

- Menú de inicio y fin, con animación del personaje y una lluvia de bananas, con movimiento y rotación controlados, para dar mejor imagen. Para esto desarrollé unos generadores de unas bananas modificadas, para que se comportaran como se buscaba.

<hr>

### Inspiración

Hace un tiempo realicé un juego en 2d de plataformas, y tuve ganas de hacer uno en 3d. Además que no me quedó como me hubiera gustado, y he querido hacer uno que fuera mucho mejor que el que hice en su dia, aplicando los nuevos conocimientos conseguidos a lo largo del ciclo.