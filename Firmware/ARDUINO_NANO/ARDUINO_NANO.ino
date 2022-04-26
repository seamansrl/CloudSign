#include <SoftwareSerial.h>
#include <Adafruit_NeoPixel.h>
#include <Wire.h>
#include <LiquidCrystal_I2C.h>

Adafruit_NeoPixel pixels(8, 5, NEO_GRB + NEO_KHZ800);
SoftwareSerial mySerial(11, 10);
LiquidCrystal_I2C lcd(0x27, 2, 1, 0, 4, 5, 6, 7, 3, POSITIVE);

float v1 = 4.9;
int measures = 20;
bool Action = false;
long DelayTime;
int piezoPin = 8;

float RemoteTemperature = 0.0;
String RemoteUser = "";
String Status = "";
String Leyenda = "";
bool RemoteDetected = false;

int Mode = 0;

unsigned long ESPTimeOut = 0;

String Last_Line1 = "";
String Last_Line2 = "";

int Line1_Position = 0;
int Line2_Position = 0;

void PrintLCD(String Line1 = "", String Line2 = "")
{
  String Temporal_Line1 = "";
  String Temporal_Line2 = "";
  bool NeedReferesh = false;

  if (Last_Line1 != Line1 || Last_Line2 != Line2)
  {
    if (Line1 == "" && Line2 == "")
      lcd.clear();
    else
    {
      if (Line1 == "$LAST")
        Line1 = Last_Line1;

      if (Line2 == "$LAST")
        Line2 = Last_Line2;

      if (Last_Line1 != Line1)
      {
        Line1_Position = 0;
        NeedReferesh = true;
        lcd.home();
        lcd.print("                ");
      }

      if (Last_Line2 != Line2)
      {
        Line2_Position = 0;
        NeedReferesh = true;
        lcd.setCursor(0, 1);
        lcd.print("                ");
      }

      Temporal_Line1 = Line1;
      Temporal_Line2 = Line2;
    }

    Last_Line1 = Line1;
    Last_Line2 = Line2;
  }

  if (Line1.length() > 16)
  {
    if (Line1_Position > 5)
    {
      Temporal_Line1 = String(Line1 + "                     ").substring(Line1_Position - 5);
      NeedReferesh = true;
      lcd.home();
      lcd.print("               ");
    }
    else
      Temporal_Line1 = Line1;

    if (Line1_Position > String(Line1 + "                     ").length() - 16)
      Line1_Position = 0;
    else
      Line1_Position++;
  }
  else
  {
    Temporal_Line1 = Line1;
  }

  if (Line2.length() > 16)
  {
    if (Line2_Position > 5)
    {
      Temporal_Line2 = String(Line2 + "                     ").substring(Line2_Position - 5);
      NeedReferesh = true;
      lcd.setCursor(0, 1);
      lcd.print("                ");
    }
    else
      Temporal_Line2 = Line2;

    if (Line2_Position > String(Line2 + "                     ").length() - 16)
      Line2_Position = 0;
    else
      Line2_Position++;
  }
  else
  {
    Temporal_Line2 = Line2;
  }

  if (NeedReferesh == true)
  {
    lcd.home();
    lcd.print(Temporal_Line1);
    lcd.setCursor(0, 1);
    lcd.print(Temporal_Line2);

    delay(250);
  }
}

String Split(String data, char separator, int index)
{
  int found = 0;
  int strIndex[] = {0, -1};
  int maxIndex = data.length() - 1;

  for (int i = 0; i <= maxIndex && found <= index; i++)
  {
    if (data.charAt(i) == separator || i == maxIndex)
    {
      found++;
      strIndex[0] = strIndex[1] + 1;
      strIndex[1] = (i == maxIndex) ? i + 1 : i;
    }
  }

  return found > index ? data.substring(strIndex[0], strIndex[1]) : "";
}

void setup()
{
  lcd.begin(16, 2);
  lcd.backlight();

  PrintLCD("INICIANDO: 0%");

  Serial.begin(57600);

  while (!Serial);
  mySerial.begin(57600);

  delay(1000);

  PrintLCD("INICIANDO: 50%");
  // PIN DE RELE
  pinMode(2, OUTPUT);
  digitalWrite(2, HIGH);

  // PIN ULTRASONICO
  pinMode(3, OUTPUT);
  pinMode(4, INPUT);


  delay(1000);

  PrintLCD("INICIANDO: 90%");
  pixels.begin();

  delay(1000);

  PrintLCD("Mire a la camara");
}

void Sound_Access()
{
  for (int loop = 0; loop <= 10; loop++)
  {
    tone(piezoPin, 1200, 20);
    delay(30);
  }
  tone(piezoPin, 1100, 50);
  delay(100);
  tone(piezoPin, 1100, 50);
  delay(200);
}

void Sound_ok()
{
  tone(piezoPin, 1000, 20);
  delay(20);
}

void Sound_noAccess()
{
  tone(piezoPin, 440, 250);
  delay(300);
  tone(piezoPin, 220, 400);
  delay(600);
}

void Neopixel_noAccess()
{
  pixels.clear();

  for (int i = 0; i < 8; i++)
  {
    pixels.setPixelColor(i, pixels.Color(255, 0, 0));

    pixels.show();

    delay(50);
  }
}


void Neopixel_access()
{
  pixels.clear();

  for (int i = 0; i < 8; i++)
  {
    pixels.setPixelColor(i, pixels.Color(0, 150, 0));

    pixels.show();

    delay(50);
  }
}

void Neopixel_off()
{
  pixels.clear();

  for (int i = 0; i < 8; i++)
    pixels.setPixelColor(i, pixels.Color(0, 0, 0));

  pixels.show();
}

void Neopixel_temp(float temperature)
{
  pixels.clear();

  if (Mode == 0)
    if (temperature < 34.0)
      for (int i = 7; i >= 0; i = i - 1)
      {
        pixels.setPixelColor(i, pixels.Color(0, 0, 255));

        pixels.show();

        delay(50);
      }

  if (temperature > 34.0)
  {
    pixels.setPixelColor(3, pixels.Color(100, 200, 100));
    pixels.setPixelColor(4, pixels.Color(100, 200, 100));
  }

  if (temperature > 35.5)
  {
    pixels.setPixelColor(2, pixels.Color(100, 200, 100));
    pixels.setPixelColor(5, pixels.Color(100, 100, 100));
  }

  if (temperature > 38)
  {
    pixels.setPixelColor(1, pixels.Color(255, 100, 40));
    pixels.setPixelColor(6, pixels.Color(255, 100, 40));
  }

  if (temperature > 39.0)
  {
    pixels.setPixelColor(0, pixels.Color(255, 0, 0));
    pixels.setPixelColor(7, pixels.Color(255, 0, 0));
  }

  if (Mode == 0)
    if (temperature > 45.0)
      for (int i = 7; i >= 0; i = i - 1)
      {
        pixels.setPixelColor(i, pixels.Color(255, 0, 0));

        pixels.show();

        delay(50);
      }

  pixels.show();
}

float GetTemperature()
{
  // MIDO EL LA TEMPERATURA
  float Grados = 0;
  float TempReaded = 0;
  float mesure = 0;
  int Ciclos = 0;

  for (int count = 0; count < measures; count++)
  {
    mesure = (((analogRead(0) * v1) / 1023.0) / 0.250) + 25;

    if (mesure >= 25.00 && mesure <= 45.00)
    {
      Grados = Grados + mesure;
      Ciclos++;
    }

    delay(5);
  }

  TempReaded = (Grados / Ciclos);

  return TempReaded;
}

int mesureDistance()
{
  long duration;
  int distance = 0;

  digitalWrite(3, LOW);
  delayMicroseconds(2);

  digitalWrite(3, HIGH);
  delayMicroseconds(10);
  digitalWrite(3, LOW);

  duration = pulseIn(4, HIGH);
  distance = duration * 0.034 / 2;

  if (distance > 50 || distance < 0)
    distance = 0;

  return distance;
}

void loop()
{
  // MIDO LA DISTANCIA AL OBJETO ENFRENTE
  int Distancia = mesureDistance();
  mySerial.print("M:" + String(Distancia) + "|");

  // MIDO AL TEMPERATURA Y LA MANDO AL ESP
  float Temperature = GetTemperature();
  mySerial.print("T:" + String(Temperature) + "|");

  // ----------------------------------


  // OBTENGO DATOS DE ARDUINO
  String IncommingFromArduino = "";
  while (Serial.available() > 0)
    IncommingFromArduino = IncommingFromArduino + String(char(Serial.read()));

  // REENVIO DATOS A LA ESP
  if (IncommingFromArduino != "")
    mySerial.print(IncommingFromArduino);


  // ----------------------------------


  // OBTENGO DATOS DEL ESP
  String IncommingFromESP = "";
  while (mySerial.available() > 0)
    IncommingFromESP = IncommingFromESP + String(char(mySerial.read()));

  // REENVIO LOS DATOS DEL ESP A LA INTERFAZ USB DEL ARDUINO
  if (IncommingFromESP != "")
  {
    ESPTimeOut = millis() + 70000;

    // REENVIO DATOS A LA ARDUINO
    Serial.print(IncommingFromESP);

    String Value = ".";
    int Index = 0;

    while (Value != "" && Index < 10)
    {
      Value = Split(IncommingFromESP, '|', Index);

      // AJUSTO EL VOLTAJE DE REFERENCIA EN CASO QUE SE AJUSTARA DESDE LA ESP
      if (Split(Value, ':', 0) == "X")
      {
        Value.replace("|", "");

        float v1_temp = Split(Value, ':', 1).toFloat();

        if (v1_temp >= 1.0 && v1_temp <= 5.0)
          v1 = v1_temp;
      }

      // ONBTENGO EL MODO
      if (Split(Value, ':', 0) == "M")
      {
        Value.replace("|", "");

        Mode = Split(Value, ':', 1).toInt();
      }

      // OBTENGO DATO DE SI SE ESTA ESCANEANDO
      if (Split(Value, ':', 0) == "D")
      {
        Value.replace("|", "");

        if (Split(Value, ':', 1).toInt() == 1)
        {
          RemoteDetected = true;
          Sound_ok();
        }
        else
          RemoteDetected = false;
      }

      // OBTENGO TEMPERATURA REMOTA
      if (Split(Value, ':', 0) == "T")
      {
        Value.replace("|", "");

        RemoteTemperature = Split(Value, ':', 1).toFloat();
      }

      // OBTENGO USUARIO DETECTADO
      if (Split(Value, ':', 0) == "U")
      {
        Value.replace("|", "");

        RemoteUser = Split(Value, ':', 1);

        if (RemoteUser == "0")
          RemoteUser = "   Bienvenido";
      }

      // OBTENGO ESTADO
      if (Split(Value, ':', 0) == "S")
      {
        Value.replace("|", "");

        Status = Split(Value, ':', 1);
      }

      // OBTENGO LEYENDA
      if (Split(Value, ':', 0) == "L")
      {
        Value.replace("|", "");

        Leyenda = Split(Value, ':', 1);
      }

      // OBTENGO ACCION
      if (Split(Value, ':', 0) == "R")
      {
        Value.replace("|", "");

        if (Split(Value, ':', 1).toInt() == 2)
        {
          Neopixel_access();

          Sound_Access();

          if (RemoteTemperature != 0.0)
            PrintLCD(RemoteUser, "Temp: " + String(RemoteTemperature) + " C");
          else if (RemoteUser != "   Bienvenido")
            PrintLCD("   Bienvenido", RemoteUser);
          else
            PrintLCD(RemoteUser, "****************");

          digitalWrite(2, LOW);
        }

        if (Split(Value, ':', 1).toInt() == 3)
        {
          Neopixel_noAccess();

          Sound_noAccess();

          PrintLCD("Acceso negado", "Temperatura alta");
        }

        if (Split(Value, ':', 1).toInt() == 4)
        {
          Neopixel_noAccess();

          Sound_noAccess();

          PrintLCD("Acceso negado", Leyenda);
        }

        if (Split(Value, ':', 2).toInt() <= 60000)
          DelayTime = Split(Value, ':', 2).toInt() + millis();
        else
          DelayTime = 60000 + millis();
            
        Action = true;
      }

      Index++;
    }
  }


  // ----------------------------------

  // MIENTRAS RECIBA PAQUETES DESDE LA ESP ESTA TODO OK, SINO PASO A MODO FUERA DE SERVICIO
  if (ESPTimeOut > millis())
  {
    // MUESTRO EL ESTADO DE TEMPERATURA SOLO SI HAY UNA DETECCION Y APAGO SOLO SI NO HAY UNA ACCION EN CURSO
    if (Action == false)
      if (RemoteDetected == true)
      {
        Neopixel_temp(Temperature);

        if (Mode == 0)
        {
          PrintLCD("Identificando...");
        }
        else if (Status == "0")
          PrintLCD("Mire a la camara", Leyenda);
        else
          PrintLCD(Status);
      }
      else
      {
        Neopixel_off();

        if (Status == "0")
          PrintLCD("Mire a la camara", Leyenda);
        else
          PrintLCD(Status);
      }


    // ----------------------------------


    // REINICIO LOS RELES
    if (DelayTime <= millis() && DelayTime > 0)
    {
      for (int Idx = 2; Idx <= 4; Idx++)
        digitalWrite(Idx, HIGH);

      DelayTime = 0;
      Action = false;
      RemoteDetected = false;
    }
    else
    {
      PrintLCD("$LAST", "$LAST");
    }
  }
  else
  {
    PrintLCD("FUERA DE SERVICIO", "Fallo en camara");
    Neopixel_off();
    Action = false;
    RemoteDetected = false;

    // REINICIO EL PUERTO COM EN CASO QUE SEA UNA FALLA DE COMUNICACION ENTRE PLACAS    
    Serial.end();
    mySerial.end();
    delay(100);
    
    Serial.begin(57600);
    while (!Serial);
    mySerial.begin(57600);
    delay(100); 
  }
}
