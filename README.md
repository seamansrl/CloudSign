# PROYECTO HORUS: CloudSign

CloudSign es el sistema de control biométrico para fichado de personal, apertura de puertas o control termino de SeaMan SRL. 

Pensado para ser de bajo costo y alta prestación, CloudSign está basada en tecnología Open hardware como la Arduino Nano y la ESP32-cam.

Funciona gracias a las API en Cloud de https://www.ProyectoHorus.com.ar pero puede ser usado también en modo Stand Alone.

 En el siguiente repositorio podrá encontrar todas las fuentes tanto para la Arduino como para la ESP32-cam como asi también los PCB en donde ambas placas se conectan, el manual de usuario, el esquema de chasis y los fuentes del administrador en red de nuestra cerradura biométrica.

También es posible descargar los programas compilados desde https://www.proyectohorus.com.ar/CloudSign como o adquirir el producto en caja cerrada.


# Elementos requeridos

- 1x ESP32-CAM Wrover Module
- 1x Arduino Nano v1/v2
- 1x Tira LED Neopixel x8 leds
- 1x Micro Rele
- 1x Pirometro Optris (Opcional)
- 1x Display Lcd 16x2 Backlight Azul 1602 Hd44780 Arduino Pic con adaptador I2C
- 1x Beeper pasivo
- 1x Memoria microSD (Formateada para FAT32)
- 1x Transformador 7,5V / 2A
- 1x Cable Pigtail Antena Conector Hembra Ipx Ufl Ard Pic para ESP32-cam
- 1x Antena 5dBi
- 1x Conector Rj45 Chasis Panel Ne8fdv Ethercon Vertical
- 1x Sensor Ultrasonico Hc-sr04 (Opcional)
- Cables
- 2x Pin Hembra Largo Female Long Pin Header 1x8p Paso (Aca se conecta la ESP32-CAM)

# Primeros pasos

Antes de empezar debemos verificar tener todos los componente, incluida nuestra PCB y el programador para la ESP32-Cam (ver Cargar el codigo en la ESP32-CAM).
Instalaremos cada componente en la PCB según se muestra en las siguientes imágenes:  

![CloudSign](img1.jpg)

![CloudSign](img2.jpg)

![CloudSign](img3.jpg)

Nota: Los pines de conexión a los elementos externos (LCD, Neo Pixel, Beeper, Etc) van del lados de las pistas. Si bien es más complicado soldar pines de esta forma, hacerlo así nos ahorra tener que hacer una PCB de dos capas ahorrando tiempo y dinero.

Otro detalle importante es notar que la ESP32-Cam está superpuesta a la Arduino, por tanto deberemos quitarle a esta última los 6 pines que sobresalen en la punta contraria al Conector USB del lado del Microcontrolador. A su vez debemos soldar los (Pin Hembra Largo Female Long Pin Header 1x8p Paso) a la PCB y poner la ESP32-Cam en ellos para darle altura.

Último detalle: si pensamos usar un gabinete metálico, deberemos usar una antena externa, pero ojo por que las ESP32-Cam tienen una resistencia que sirve para seleccionar entre el conector a pigtail y la antena integrada, es una resistencia SMD muy muy pequeña que deberemos cambiar de posición, ver este vídeo para más información (https://m.youtube.com/watch?v=ckPu18lrBkE)

Vamos a ver 5 conjuntos de pines para conexiones exteriores:

- RS232 (3 pines Negativo TX y RX)
- Tira Neopixel (3 pines Negativo, Positivo y Data)
- LCD (4 Pines Negativo, Positivo, Data y Clock)
- Beeper (2 pines Negativo y señal)
- Rele (3 Pines Negativo, Positivo y Trigger)
- Pitometro (3 Pines Negativo, Positivo y Señal analógica)
- Ultrasónico (4 Pines Negativo, Positivo, Trigger y Retorno)

# Comunicación entre placas.

Si bien es verdad que podría hacerse todos con solo la ESP32-Cam, vemos que está está un tanto limitada en pines de salida como así también tiene un serio problema con su pin Analógico-Digital el cual tiende a ser bastante inseparable, es por esto que acudimos a una Arduino Nano como soporte para el control de todos los elementos externos dejando a la ESP32-Cam con las funciones de red y calculo. Podríamos decir que la Arduino hace de placa IDE mientras que la ESP32-Cam ee el Mother, memoria y el CPU.
Para que ambas placas compartan información, usamos los pines RS323 mandando mensajes cortos separados por pipe (|) en donde tenernos el comando seguido de los parámetros. La comunicación si bien es bi-direccional en el sentido de que ambas placas se comparten datos, el proceso es tipo BroadCast ya que no se espera confirmación de recepción. 
Es importante no cambiar la velocidad de comunicación entre las placas, ya que la codificada es la velocidad nativa, velocidades menores o mayores devendrán en errores.

# Cargar el codigo en la ESP32-CAM

Para copiar el codigo a la ESP32-Cam se requiere de un adaptador USB-TTL el cual ira conectado de la siguiente manera:

![Conexion entre TTL y ESP32-Cam](Conexionado.jpg)

La configuración del entorno arduino para la carga sera:

![Configuracion en entorno Arduino](Config_Arduino.png)


Nota: Si hay problemas para subir el codigo a la placa y todo parace corresponder correctamente debera probar invirtiendo el RX y el TX de la placa TTL.


# Dentro de las funciones de la API para CloudSign podemos encontrar

El Proyecto Horus consiste en una API REST que permite de forma simple identificar imágenes vía redes neuronales.

- FACE ID
- FACE MASK

Administra la API directo desde tu back usando la documentación en Swagger https://www.proyectohorus.com.ar/Documentacion/Administrador.json

# Seguime en:
https://www.linkedin.com/in/fernando-p-maniglia/

# Conocenos más en:
https://www.seamansrl.com.ar
