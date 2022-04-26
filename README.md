# PROYECTO HORUS: Demo en Arduino C para ESP32-CAM

Código demo en C de ARDUINO para el consumo de la API de reconocimiento (Proyecto Horus)

El Proyecto Horus consiste en una API REST que permite de forma simple identificar imagenes via redes neuronales.

# Cargar el codigo en la ESP32-CAM

Para copiar el codigo a la ESP32-Cam se requiere de un adaptador USB-TTL el cual ira conectado de la siguiente manera:

![Conexion entre TTL y ESP32-Cam](Conexionado.jpg)

La configuración del entorno arduino para la carga sera:

![Configuracion en entorno Arduino](Config_Arduino.png)


Nota: Si hay problemas para subir el codigo a la placa y todo parace corresponder correctamente debera probar invirtiendo el RX y el TX de la placa TTL.


# Dentro de las funciones de la API podemos encontrar

El Proyecto Horus consiste en una API REST que permite de forma simple identificar imágenes vía redes neuronales.

- FACE ID
- OBJECT DETECTION
- QR DECODER
- ID DECODER
- APLR (AUTOMATIC PLATE LICENSE RECOGNITION)

Administra la API usando la app instalable 
https://www.proyectohorus.com.ar/descargas/windows/admin.zip

Administra la API usando nuestro administrador web 
https://www.proyectohorus.com.ar/administrador

Administra la API directo desde tu back usando la documentación en Swagger https://www.proyectohorus.com.ar/Documentacion/Administrador.json

La URL a usar en el codigo de ejemplo es:
https://server1.proyectohorus.com.ar

El usuario, Password y Perfil se obtienen en esta primer etapa desde el software descargable.

Ejemplo de como usar el administrador aca:

https://www.youtube.com/watch?v=pf7yy0KpRks&t=3s

# Contactanos en:
https://www.linkedin.com/company/35599193/admin/

# Seguime en:
https://www.linkedin.com/in/fernando-p-maniglia/

# Conocenos más en:
https://www.seamansrl.com.ar
