#include <esp_wifi.h>
#include "esp_http_server.h"
#include "esp_http_client.h"
#include "esp_camera.h"
#include <HTTPClient.h>
#include "fd_forward.h"
#include "FS.h" 
#include "SD_MMC.h" 
#include <ArduinoJson.h>
#include "soc/soc.h"
#include "soc/rtc_cntl_reg.h"
#include <ESP32httpUpdate.h>
#include <NTPClient.h>    
#include <TimeLib.h>
#include "ESP32FtpServer.h"
#include "esp_wps.h"
#include "EEPROM.h"
#include "img_converters.h"

#define FRAME_SIZE FRAMESIZE_QVGA
#define BLOCK_SIZE 10
#define WIDTH 320
#define HEIGHT 240
#define W (WIDTH / BLOCK_SIZE)
#define H (HEIGHT / BLOCK_SIZE)

// ESTRUCTURAS
struct Http_Response{
  int Status;
  String Data;
};

// MOTION DETECTION
float BLOCK_DIFF_THRESHOLD = 0.10;
float IMAGE_DIFF_THRESHOLD = 0.20;
uint16_t prev_frame[H][W] = { 0 };
uint16_t current_frame[H][W] = { 0 };

// RELOJ NTP
WiFiUDP ntpUDP;
NTPClient timeClient(ntpUDP, "1.south-america.pool.ntp.org");
int ClockOffset = 0;

// CONFIG FTP SERVER
FtpServer ftpSrv;
String FTPUser = "admin";
String FTPPass = "CloudSign";
        
// CONFIG
boolean TemNotRequired = false;
boolean IDNotRequired = true;
boolean FaceNotRequired = false;
boolean UltrasonicRequired = false;

// GENERAL
String DeviceName = "";
String CustomVars = "";
String LastHour = "";
boolean FlancoTemp = false;
boolean FlancoFace = false;
boolean FlancoId = false;
boolean FaceFound = true;
float VoltageRef = 0.0;
String ConfigFile = "";
boolean SDReady =  false;
boolean WPSReady = false;
boolean matrix3du_Ready = false;
boolean CamaraReady = false;
String LastConfigFile = "";
boolean Acceso = false;
float TempTrigger = 0.05;
boolean DebugMode = false;

int ActionWait = 5000;
int WaitValue = 0;
int MinBoxSize = 35;
String Leyenda = "";
String FromTTL = "";
int TTLTimeOut = 10000;
long Last_TTLTimeOut = 0;
int UltrasonicMinDistance = 35;

// WEB SERVER
String User = "";
String Status = "";
String Function = "";
String UUID = "";
String Id = "";
int AccessFrom = 0;
int AccessTo = 0;
bool AccessDay[6];
bool UserActive = false;
bool ConfigMode = false;

httpd_handle_t camara_httpd = NULL;

typedef struct {
        httpd_req_t *req;
        size_t len;
} jpg_chunking_t;

// PIROMETRO
float LastTemp = 0.0;
float Temp = 0.0;
float MinTemp = 33;
float MaxTemp = 42;
float BodyTemp = 36;
float AlertTemp = 40;
float Offset = 0;

// WIFI
String ssid = "";
String password = "";
bool internet_connected = false;
long last_capture_millis = 0;
long last_motion_millis = 0;

// PARA OBTENER EL UUID DEL PERFIL COMO EN USUARIO Y LA CLAVE PODES ENTRAR EN http://www.proyectohorus.com.ar Y BAJAR EL ADMINISTRADOR
String APItoken = "";
int capture_interval = 10;
bool Ready = true;

// FACE DETECTION PARAMATER
int EdgeDetector_min_face = 80;
float EdgeDetector_pyramid = 0.707;
int EdgeDetector_pyramid_times = 4;
float Pnet_score = 0.6;
float Pnet_mns = 0.7;
int Pnet_candidate_number = 10;
float Rnet_score = 0.7;
float Rnet_mns = 0.7;
int Rnet_candidate_number = 5;
float Onet_score = 0.7;
float Onet_mns = 0.7;
int Onet_candidate_number = 1;
  
// HORUS WEB SERVICE
int HorusMode = 0;
int HorusAuth = 0;
String APIprofileuuid = "";
String APIUser = "";
String APIPassword = "";

// CAMARA SETINGS
int brightness = 0;         // -2 to 2
int contrast = 0;           // -2 to 2
int saturation = 0;         // -2 to 2
int special_effect = 0;     // 0 to 6 (0 - No Effect, 1 - Negative, 2 - Grayscale, 3 - Red Tint, 4 - Green Tint, 5 - Blue Tint, 6 - Sepia)
int whitebal = 1;           // 0 = disable , 1 = enable
int awb_gain = 1;           // 0 = disable , 1 = enable
int wb_mode = 0;            // 0 to 4 - if awb_gain enabled (0 - Auto, 1 - Sunny, 2 - Cloudy, 3 - Office, 4 - Home)
int exposure_ctrl = 1;      // 0 = disable , 1 = enable
int aec2 = 1;               // 0 = disable , 1 = enable
int ae_level = 2;           // -2 to 2
int aec_value = 1200;       // 0 to 1200
int gain_ctrl = 1;          // 0 = disable , 1 = enable
int agc_gain = 30;          // 0 to 30
int gainceiling = 2;        // 0 to 6
int bpc = 1;                // 0 = disable , 1 = enable
int wpc = 1;                // 0 = disable , 1 = enable
int raw_gma = 1;            // 0 = disable , 1 = enable
int lenc = 1;               // 0 = disable , 1 = enable
int hmirror = 0;            // 0 = disable , 1 = enable
int vflip = 0;              // 0 = disable , 1 = enable
int dcw = 1;                // 0 = disable , 1 = enable
int colorbar = 0;           // 0 = disable , 1 = enable

// CAMERA_MODEL_AI_THINKER
#define PWDN_GPIO_NUM     32
#define RESET_GPIO_NUM    -1
#define XCLK_GPIO_NUM      0
#define SIOD_GPIO_NUM     26
#define SIOC_GPIO_NUM     27
#define Y9_GPIO_NUM       35
#define Y8_GPIO_NUM       34
#define Y7_GPIO_NUM       39
#define Y6_GPIO_NUM       36
#define Y5_GPIO_NUM       21
#define Y4_GPIO_NUM       19
#define Y3_GPIO_NUM       18
#define Y2_GPIO_NUM        5
#define VSYNC_GPIO_NUM    25
#define HREF_GPIO_NUM     23
#define PCLK_GPIO_NUM     22


// FACE ID EDGE
static inline mtmn_config_t app_mtmn_config()
{
  mtmn_config_t mtmn_config = {0};
  mtmn_config.type = FAST;
  mtmn_config.min_face = EdgeDetector_min_face;
  mtmn_config.pyramid = EdgeDetector_pyramid;
  mtmn_config.pyramid_times = EdgeDetector_pyramid_times;
  
  mtmn_config.p_threshold.score = Pnet_score;
  mtmn_config.p_threshold.nms = Pnet_mns;
  mtmn_config.p_threshold.candidate_number = Pnet_candidate_number;
  
  mtmn_config.r_threshold.score = Rnet_score;
  mtmn_config.r_threshold.nms = Rnet_mns;
  mtmn_config.r_threshold.candidate_number = Rnet_candidate_number;
  
  mtmn_config.o_threshold.score = Onet_score;
  mtmn_config.o_threshold.nms = Onet_mns;
  mtmn_config.o_threshold.candidate_number = Onet_candidate_number;
  
  return mtmn_config;
}
mtmn_config_t mtmn_config = app_mtmn_config();
static dl_matrix3du_t *image_matrix = NULL;
static box_array_t *net_boxes = NULL;
camera_config_t config;
camera_fb_t * fb = NULL;

// OTA CONFIG
const int FW_VERSION = 160; 
const char* fwUrlBase = "http://www.proyectohorus.com.ar/updates/pirometro/v1/";

// POST DATA ACTIONS
String OK_URL = "";
String FAIL_TMP_URL = "";
String FAIL_UNKNOW_URL = "";
String TTL_URL = "";
String OK_VERB = "GET";
String FAIL_TMP_VERB = "GET";
String FAIL_UNKNOW_VERB = "GET";
String TTL_VERB = "GET";

//WPS
#define ESP_WPS_MODE      WPS_TYPE_PBC
#define ESP_MANUFACTURER  "SEAMAN"
#define ESP_MODEL_NUMBER  "001"
#define ESP_MODEL_NAME    "BIOMETRIC"
#define ESP_DEVICE_NAME   "CLOUD SIGN"

static esp_wps_config_t configWPS;

// BASIC FUNCTIONS
void SendMessage(String Message = "")
{
  if (Message != "")
  {
    Serial.print(Message);
  
    if (Split(Message, ':', 0) == "S")
      if (Message == "S:0|")
        delay(100);
      else
        if (DebugMode)
          delay(20);
        else
          delay(1000);
    else
      delay(200);
  }
}

boolean IsResete(boolean CleanMemory = false)
{
  if (DebugMode)
    SendMessage("S:IsResete");
    
  try
  {  
    if (EEPROM.begin(2) == true)
    {
      if (CleanMemory == true)
      {
          EEPROM.write(0, 0);
          EEPROM.end();

          return false;
      }
      else
      {
        int Previus = int(EEPROM.read(0));

        if (Previus >= 3)
        {
          EEPROM.write(0, 0);
          EEPROM.end();
          
          return true; 
        }
        else
        {
          Previus = Previus + 1;

          EEPROM.write(0, Previus);
          EEPROM.end();

          return false; 
        }
      }
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: IsResete");
  }

  return false;
}

char* string2char(String command)
{
    if (DebugMode)
      SendMessage("S:string2char");
    
    if(command.length()!=0){
        char *p = const_cast<char*>(command.c_str());
        return p;
    }
}

String Split(String data, char separator, int index)
{
  try
  {   
      int found = 0;
      int strIndex[] = {0, -1};
      int maxIndex = data.length()-1;
      
      for(int i=0; i<=maxIndex && found<=index; i++)
      {
            if(data.charAt(i)==separator || i==maxIndex)
            {
                  found++;
                  strIndex[0] = strIndex[1]+1;
                  strIndex[1] = (i == maxIndex) ? i+1 : i;
            }
      }
      
      return found>index ? data.substring(strIndex[0], strIndex[1]) : "";
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: Split");

    return "";  
  }
}

void CronConfig(fs::FS &fs, int Hour){
  try
  {
    if (DebugMode)
        SendMessage("S:CronConfig");
        
    File root = fs.open("/cron");
    
    if(!root)
      return;

    if(!root.isDirectory())
      return;


    File file = root.openNextFile();
    
    while(file)
    {
        if(! file.isDirectory())
        {
            String From = Split(Split(Split(file.name(),'/',2),'.',0),'-',0);
            String To = Split(Split(Split(file.name(),'/',2),'.',0),'-',1);

            if (From != To && From != "" && To != "")
            {
              if (Hour >= From.toInt() && Hour <= To.toInt())
              {
                String Puntero = From + "-" + To;
                
                if (Puntero != LastConfigFile)
                {
                  LastConfigFile = Puntero;
                  
                  GetConfig(SD_MMC, file.name());
                }

                return;
              }
            }
        }
        
        file = root.openNextFile();
    }

    if (LastConfigFile != "")
    {
      LastConfigFile = "";
    
      GetConfig(SD_MMC, "/config.json");
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: CronConfig");
  }
}

float Scale(fs::FS &fs, const char * path, float Input)
{
  try
  {
    if (SDReady == true)
    {
      File file = fs.open(path);
      if(file)
      {
        ConfigFile = "";
        
        while(file.available())
            ConfigFile = ConfigFile + String(char(file.read()));
    
        file.flush();
        file.close();
        
        const size_t bufferSize = ConfigFile.length() + 1024;
        
        DynamicJsonDocument doc(bufferSize);
        deserializeJson(doc, ConfigFile);
        JsonObject obj = doc.as<JsonObject>();

        if (ConfigFile != "")
        {
          float TemperatureScaled = obj[String(Input,1)].as<float>();
          if (TemperatureScaled > 0)
          {
            return TemperatureScaled;
          }
          else
          {
            return Input;
          }
        }
      }
    }

    return Input;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("D:0|");
    SendMessage("S:ERROR ON SCALE|");
    return Input;
  }
}

void GetConfig(fs::FS &fs, const char * path)
{
  try
  {
    if (DebugMode)
        SendMessage("S:GetConfig");
        
    if (SDReady == false)
    {
      SendMessage("D:0|");
      SendMessage("S:NO SD|");
      return;
    }
     
    File file = fs.open(path);
    if(!file)
    {
        SendMessage("D:0|");
        SendMessage("S:CONFIG FAIL|");
        return;
    }

    ConfigFile = "";
    
    while(file.available())
        ConfigFile = ConfigFile + String(char(file.read()));

    file.flush();
    file.close();
    
    const size_t bufferSize = ConfigFile.length() + 1024;
    
    DynamicJsonDocument doc(bufferSize);
    deserializeJson(doc, ConfigFile);
    JsonObject obj = doc.as<JsonObject>();


    if (ConfigFile != "")
    {
      SendMessage("D:0|");
      SendMessage("S:SETTING|");
      Status = "SETTING";
              
      // LECTURA DE DATOS A VARIABLES GLOBALES
      APItoken = "";
      
      ssid =                       obj["ssid"].as<String>();
      password =                   obj["password"].as<String>();
      
      APIprofileuuid =             obj["api_profile_uuid"].as<String>();
      APIUser =                    obj["api_user"].as<String>();
      APIPassword =                obj["api_password"].as<String>();
      
      TemNotRequired =             obj["tem_not_required"].as<bool>();
      IDNotRequired =              obj["id_not_required"].as<bool>();
      FaceNotRequired =            obj["face_not_required"].as<bool>();
      UltrasonicRequired =         obj["ultrasonic_required"].as<bool>();

      DebugMode =                  obj["debug_mode"].as<bool>();
      
      MinTemp =                    obj["min_temp"].as<float>();
      MaxTemp =                    obj["max_temp"].as<float>();
      BodyTemp =                   obj["body_temp"].as<float>();
      AlertTemp =                  obj["alert_temp"].as<float>();
      Offset =                     obj["offset"].as<float>();
      TempTrigger =                obj["temp_trigger"].as<float>();
      UltrasonicMinDistance =      obj["ultrasonic_min_distance"].as<int>();
      
      ActionWait =                 obj["action_wait"].as<int>();
      
      MinBoxSize =                 obj["min_box_size"].as<int>();
      
      EdgeDetector_min_face =      obj["edge_detector_min_face"].as<int>();
      EdgeDetector_pyramid =       obj["edge_detector_pyramid"].as<float>();
      EdgeDetector_pyramid_times = obj["edge_detector_pyramid_times"].as<int>();
            
      Pnet_score =                 obj["pnet_score"].as<float>();
      Pnet_mns =                   obj["pnet_mns"].as<float>();
      Pnet_candidate_number =      obj["pnet_candidate_number"].as<int>();
      
      Rnet_score =                 obj["rnet_score"].as<float>();
      Rnet_mns =                   obj["rnet_mns"].as<float>();
      Rnet_candidate_number =      obj["rnet_candidate_number"].as<int>();
      
      Onet_score =                 obj["onet_score"].as<float>();
      Onet_mns =                   obj["onet_mns"].as<float>();
      Onet_candidate_number =      obj["onet_candidate_number"].as<int>();
      
      OK_URL =                     obj["ok_url"].as<String>();
      OK_VERB =                    obj["ok_verb"].as<String>();;
      FAIL_TMP_URL =               obj["fail_tmp_url"].as<String>();
      FAIL_TMP_VERB =              obj["fail_temp_verb"].as<String>();
      FAIL_UNKNOW_URL =            obj["fail_Unknow_url"].as<String>();
      FAIL_UNKNOW_VERB =           obj["fail_unknow_verb"].as<String>();
      TTL_URL =                    obj["ttl_url"].as<String>();
      TTL_VERB =                   obj["ttl_verb"].as<String>();

      HorusMode =                  obj["horus_mode"].as<int>();
      HorusAuth =                  obj["horus_auth"].as<int>();
      
      DeviceName =                 obj["device_name"].as<String>();
      ClockOffset =                obj["clock_offset"].as<int>();
      TTLTimeOut =                 obj["ttl_timeout"].as<int>();

      BLOCK_DIFF_THRESHOLD =       obj["block_diff"].as<float>();
      IMAGE_DIFF_THRESHOLD =       obj["image_diff"].as<float>();

      CustomVars =                 obj["custom_vars"].as<String>();
      VoltageRef =                 obj["voltage_ref"].as<float>();

      FTPUser  =                   obj["ftp_user"].as<String>();
      FTPPass  =                   obj["ftp_password"].as<String>();
      
      if (obj["PersonalCam"].as<bool>() == true)
      {
        brightness = obj["brightness"].as<int>();
        contrast = obj["contrast"].as<int>();
        saturation = obj["saturation"].as<int>();
        special_effect = obj["special_effect"].as<int>();
        whitebal = obj["whitebal"].as<int>();
        awb_gain = obj["awb_gain"].as<int>();
        wb_mode = obj["wb_mode"].as<int>();
        exposure_ctrl = obj["exposure_ctrl"].as<int>();
        aec2 = obj["aec2"].as<int>();
        ae_level = obj["ae_level"].as<int>();
        aec_value = obj["aec_value"].as<int>();
        gain_ctrl = obj["gain_ctrl"].as<int>();
        agc_gain = obj["agc_gain"].as<int>();
        gainceiling = obj["gainceiling"].as<int>();
        bpc = obj["bpc"].as<int>();
        wpc = obj["wpc"].as<int>();
        raw_gma = obj["raw_gma"].as<int>();
        lenc = obj["lenc"].as<int>();
        hmirror = obj["hmirror"].as<int>();
        vflip = obj["vflip"].as<int>();
        dcw = obj["dcw"].as<int>();
        colorbar = obj["colorbar"].as<int>();

        if (CamaraReady == true)
          SetCamara();
      }
      
      // DEFAULT CONFIG
      FaceFound = IDNotRequired;

      if (UltrasonicMinDistance == 0)
        UltrasonicMinDistance = 35;

      if (TempTrigger == 0)
        TempTrigger = 0.05;
        
      if (BLOCK_DIFF_THRESHOLD == 0);
        BLOCK_DIFF_THRESHOLD = 0.20;
        
      if (IMAGE_DIFF_THRESHOLD == 0)
        IMAGE_DIFF_THRESHOLD = 0.10;

      if (FTPUser == "null" || FTPUser == "")
        FTPUser = "admin";

      if (FTPPass == "null" || FTPPass == "")
        FTPPass = "CloudSign";
        
      if (OK_VERB == "null" || OK_VERB == "")
        OK_VERB = "GET";
        
      if (FAIL_TMP_VERB == "null" || FAIL_TMP_VERB == "")
        FAIL_TMP_VERB = "GET";

      if (FAIL_UNKNOW_VERB == "null" || FAIL_UNKNOW_VERB == "")
        FAIL_UNKNOW_VERB = "GET";

      if (TTL_VERB == "null" || TTL_VERB == "")
        TTL_VERB = "GET";

      if (OK_URL == "null")
        OK_URL = "";

      if (FAIL_TMP_URL == "null")
        FAIL_TMP_URL = "";

      if (FAIL_UNKNOW_URL == "null")
        FAIL_UNKNOW_URL = "";
      
      if (TTL_URL == "null")
        TTL_URL = "";

      if (DeviceName == "null")
        DeviceName = "Horus_Cam";

      if (TTLTimeOut == 0)
        TTLTimeOut = 10000;

      if (Pnet_score == 0)
        Pnet_score = 0.6;

      if (Pnet_mns == 0)  
        Pnet_mns = 0.7;

      if (Pnet_candidate_number == 0 )
        Pnet_candidate_number = 20;

      if (Rnet_score == 0)
        Rnet_score = 0.7;

      if (Rnet_mns == 0)
        Rnet_mns = 0.7;

      if (Rnet_candidate_number == 0)
        Rnet_candidate_number = 10;

      if (Onet_score == 0)
        Onet_score = 0.7;

      if (Onet_mns == 0)
        Onet_mns = 0.7;

      if (Onet_candidate_number == 0)
        Onet_candidate_number = 1;

      if (HorusMode == 0)
        HorusMode = 0;
        
      if (EdgeDetector_min_face == 0)
        EdgeDetector_min_face = 40;

      if (EdgeDetector_pyramid == 0)
        EdgeDetector_pyramid = 0.707;

      if (EdgeDetector_pyramid == 0)
        EdgeDetector_pyramid_times = 4;
        
      if (MinBoxSize == 0)
        MinBoxSize = 35;
      
      if (ActionWait == 0)
        ActionWait = 5000;
        
      if (MinTemp == 0.0)
        MinTemp = 33.00;

      if (MaxTemp == 0.0)
        MaxTemp = 41.00;

      if (BodyTemp == 0.0)
        BodyTemp = 36;

      if (AlertTemp == 0.0)
        AlertTemp = 39.5;

      if (ssid == "null")
        ssid = "";
    
      if (password == "null")
        password = "";        
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: GetConfig");
  }
}

boolean isNumeric(String str) 
{
  try
  {
    if (DebugMode)
        SendMessage("S:isNumeric");
        
    unsigned int stringLength = str.length();
 
    if (stringLength == 0) {
        return false;
    }
 
    boolean seenDecimal = false;
 
    for(unsigned int i = 0; i < stringLength; ++i) {
        if (isDigit(str.charAt(i))) {
            continue;
        }
 
        if (str.charAt(i) == '.') {
            if (seenDecimal) {
                return false;
            }
            seenDecimal = true;
            continue;
        }
        return false;
    }
    return true;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: IsNumeric");

    return false;  
  }
}

String ip2Str(IPAddress ip)
{
  try
  {
    if (DebugMode)
        SendMessage("S:ip2Str");
        
    String s="";
    for (int i=0; i<4; i++) 
      s += i  ? "." + String(ip[i]) : String(ip[i]);
    
    return s;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: ip2Str");

    return "127.0.0.1";  
  }
}

void checkForUpdates() 
{
  try
  {
    if (DebugMode)
        SendMessage("S:checkForUpdates");
        
    if (WiFi.status() == WL_CONNECTED && internet_connected == true)
    {
      String fwVersionURL = fwUrlBase;
      fwVersionURL.concat("ver.txt");
      String fwImageURL = fwUrlBase;
      fwImageURL.concat("firmware.bin");
      
      HTTPClient httpClient;
      httpClient.begin( fwVersionURL );
      
      int httpCode = httpClient.GET();
      
      if( httpCode == 200 ) 
      {
        int newVersion = httpClient.getString().toInt();
        
        if( newVersion > FW_VERSION ) 
        {
          SendMessage("S:UPDATING FIRMWARE|");
  
          t_httpUpdate_return ret = ESPhttpUpdate.update( fwImageURL );
  
          switch(ret) 
          {
              case HTTP_UPDATE_FAILED:
                  SendMessage("S:UPDATE FAILD|");
                  break;
  
              case HTTP_UPDATE_NO_UPDATES:
                  SendMessage("S:NO UPDATES|");
                  break;
  
              case HTTP_UPDATE_OK:
                  SendMessage("S:UPDATE OK|");
                  break;
          }
        }
      }
      else
      {
        SendMessage("S:CHECK FAIL|");  
      }
      
      httpClient.end();
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: CheckForUpdates");
  }
}

Http_Response SendHTTPMessage(String URL, String Metodo = "GET")
{
  if (DebugMode)
        SendMessage("S:SendHTTPMessage");
        
  Http_Response Response;

  Response.Status = 0;
  Response.Data = "";
  
  try
  {
    if (URL != "")
    {
      if (WiFi.status() == WL_CONNECTED && internet_connected == true)
      {
        HTTPClient http;

        http.setTimeout(5000);
        http.begin(string2char(URL)); 
        
        if (Metodo == "GET")
        {
          Response.Status = http.GET();
          Response.Data = http.getString();
        }
        else
        {
          Response.Status = http.POST("");
          Response.Data = http.getString();
        }
        
        http.end();
      }
    }
    else
    {
      Response.Status = 0;
      Response.Data = ""; 
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: SendHTTPMessage");
    
    Response.Status = 0;
    Response.Data = "";
  }

  return Response;
}

// WPS
void wpsInitConfig()
{
  if (DebugMode)
        SendMessage("S:wpsInitConfig");
        
  configWPS.crypto_funcs = &g_wifi_default_wps_crypto_funcs;
  configWPS.wps_type = ESP_WPS_MODE;
  strcpy(configWPS.factory_info.manufacturer, ESP_MANUFACTURER);
  strcpy(configWPS.factory_info.model_number, ESP_MODEL_NUMBER);
  strcpy(configWPS.factory_info.model_name, ESP_MODEL_NAME);
  strcpy(configWPS.factory_info.device_name, ESP_DEVICE_NAME);
}

String wpspin2string(uint8_t a[])
{
  if (DebugMode)
        SendMessage("S:wpspin2string");
        
  char wps_pin[9];
  for(int i=0;i<8;i++){
    wps_pin[i] = a[i];
  }
  wps_pin[8] = '\0';
  return (String)wps_pin;
}

void WiFiEvent(WiFiEvent_t event, system_event_info_t info)
{
  if (DebugMode)
        SendMessage("S:WiFiEvent");
        
  switch(event){
    case SYSTEM_EVENT_STA_START:
      SendMessage("S:STATION MODE|");
      break;
    case SYSTEM_EVENT_STA_GOT_IP:
      SendMessage("S:SSID " + String(WiFi.SSID() + "|"));
      SendMessage("S:IP " + ip2Str(WiFi.localIP()) + "|");
      break;
    case SYSTEM_EVENT_STA_DISCONNECTED:
      SendMessage("S:RECONNECTING|");
      WiFi.reconnect();
      break;
    case SYSTEM_EVENT_STA_WPS_ER_SUCCESS:
      SendMessage("S:WPS SUCCESSFULL " + String(WiFi.SSID() + "|"));
      esp_wifi_wps_disable();
      delay(10);
      WiFi.begin();
      break;
    case SYSTEM_EVENT_STA_WPS_ER_FAILED:
      SendMessage("S:WPS Failed, retrying|");
      esp_wifi_wps_disable();
      esp_wifi_wps_enable(&configWPS);
      esp_wifi_wps_start(0);
      break;
    case SYSTEM_EVENT_STA_WPS_ER_TIMEOUT:
      SendMessage("S:WPS TIMEOUT|");
      esp_wifi_wps_disable();
      esp_wifi_wps_enable(&configWPS);
      esp_wifi_wps_start(0);
      break;
    case SYSTEM_EVENT_STA_WPS_ER_PIN:
      SendMessage("S:WPS_PIN = " + wpspin2string(info.sta_er_pin.pin_code) + "|");
      break;
    default:
      break;
  }
}

// JSON WEB
static esp_err_t data_handler(httpd_req_t *req)
{
  try
  {
    if (DebugMode)
        SendMessage("S:esp_err_t data_handler");
        
    static char json_response[1024];
    static char CopyString[1024];
  
    char * p = json_response;
    char * copy = CopyString;
  
    *p++ = '{';
          
    p+=sprintf(p, "\"status\":\"%s\",", string2char(Status));
    p+=sprintf(p, "\"mode\":\"%s\",", string2char(String(HorusMode)));                              
    p+=sprintf(p, "\"user\":\"%s\",", string2char(User));
    p+=sprintf(p, "\"temp\":%f,", Temp);
  
    if (FlancoFace == true)
      p+=sprintf(p, "\"detected\":\"%s\",", "true");
    else
      p+=sprintf(p, "\"detected\":\"%s\",", "false");
    
    if (Acceso)
      p+=sprintf(p, "\"acceso\":\"%s\"", "true");
    else
      p+=sprintf(p, "\"acceso\":\"%s\"", "false");
  
      
    *p++ = '}';
    *p++ = 0;
      
    httpd_resp_set_type(req, "application/json");
    httpd_resp_set_hdr(req, "Access-Control-Allow-Origin", "*");
    
    return httpd_resp_send(req, json_response, strlen(json_response));
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: data_handler");
  }
}

// PICTURE WEB
static esp_err_t picture_handler(httpd_req_t *req)
{
  try
  {
    if (DebugMode)
        SendMessage("S:esp_err_t picture_handler");
        
    esp_err_t res = ESP_OK;
  
    if (!fb)
      return res;
    
    httpd_resp_set_type(req, "image/jpeg");
    httpd_resp_set_hdr(req, "Content-Disposition", "inline; filename=capture.jpg");
    httpd_resp_set_hdr(req, "Access-Control-Allow-Origin", "*");
  
    size_t fb_len = 0;

    fb_len = fb->len;
    res = httpd_resp_send(req, (const char *)fb->buf, fb->len);

    return res;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: picture_handler");
  }
}

// CONFIG FILE
static esp_err_t config_handler(httpd_req_t *req)
{
  try
  {     
    if (DebugMode)
        SendMessage("S:esp_err_t config_handler");
        
    String ConfigStatus = "";
    
    if (ConfigMode == true)
    {
      ConfigStatus = "{\"config_mode\":false}";
      ConfigMode = false;
      GetConfig(SD_MMC, "/config.json");
    }
    else
    {
      ConfigStatus = "{\"config_mode\":true}";
      ConfigMode = true;
    }
    

    httpd_resp_set_type(req, "application/json");
    httpd_resp_set_hdr(req, "Access-Control-Allow-Origin", "*");
    
    return httpd_resp_send(req, string2char(ConfigStatus), strlen(string2char(ConfigStatus)));
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: config_handler");
  }
}

// INFO DATA
static esp_err_t info_handler(httpd_req_t *req)
{
  try
  {     
    if (DebugMode)
        SendMessage("S:esp_err_t info_handler");
        
    String InfoStatus = "";
    String ConfigStatus = "";
    
    if (ConfigMode == true)
    {
      ConfigStatus = " ,\"config_mode\":true";
    }
    else
    {
      ConfigStatus = " ,\"config_mode\":false";
    }
    
    InfoStatus = "{\"device\":\"CloudSign\", \"device_name\":\"" + String(DeviceName) + "\", \"ip\":\"" + ip2Str(WiFi.localIP()) + "\", \"version\":" + String(FW_VERSION) + ConfigStatus + "}"; 

    httpd_resp_set_type(req, "application/json");
    httpd_resp_set_hdr(req, "Access-Control-Allow-Origin", "*");
    
    return httpd_resp_send(req, string2char(InfoStatus), strlen(string2char(InfoStatus)));
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: info_handler");
  }
}

// WEB SERVER
void startCameraServer()
{
  try
  {
    if (DebugMode)
        SendMessage("S:startCameraServer");
        
    httpd_config_t config = HTTPD_DEFAULT_CONFIG();
    config.server_port = 80;

    httpd_uri_t info_uri = {
      .uri       = "/info",
      .method    = HTTP_GET,
      .handler   = info_handler,
      .user_ctx  = NULL
    };
    
    httpd_uri_t image_uri = {
      .uri       = "/image",
      .method    = HTTP_GET,
      .handler   = picture_handler,
      .user_ctx  = NULL
    };
  
    httpd_uri_t data_uri = {
      .uri       = "/data",
      .method    = HTTP_GET,
      .handler   = data_handler,
      .user_ctx  = NULL
    };

    httpd_uri_t config_uri = {
      .uri       = "/config",
      .method    = HTTP_GET,
      .handler   = config_handler,
      .user_ctx  = NULL
    };
    
    if (httpd_start(&camara_httpd, &config) == ESP_OK) {
      httpd_register_uri_handler(camara_httpd, &image_uri);
      httpd_register_uri_handler(camara_httpd, &data_uri);
      httpd_register_uri_handler(camara_httpd, &config_uri);
      httpd_register_uri_handler(camara_httpd, &info_uri);
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: startCameraServer");
  }
}

// HORUS API TOKEN
String GetToken(String user, String passwd, String profileuuid)
{
  try
  {
    if (DebugMode)
        SendMessage("S:GetToken");
        
    String Token = "";
    
    HTTPClient http;

    http.setTimeout(5000);
    http.begin("http://server1.proyectohorus.com.ar/api/v2/functions/login"); 
    
    http.addHeader("Content-Type", "application/x-www-form-urlencoded");
    int httpCode = http.POST("user=" + user + "&password=" + passwd + "&profileuuid=" + profileuuid);
    
    if (httpCode > 0) 
    {
          String payload = http.getString();
          
          if (Split(payload,'|',0) == "200")
          {
            Token = "Bearer " + Split(payload,'|',1);
          }
          else
          {
            SendMessage("S:ERR A-" + (String)Split(payload,'|',0) + "|");
            Status = "ERR A-" + (String)Split(payload,'|',0);
        
            Token = "";  
          }
    }
    else
    {
      SendMessage("S:ERR B-" + (String)httpCode + "|");
      Status = "ERR B-" + (String)httpCode;
  
      Token = "";  
    }
    
    http.end();
    
    return Token;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: GetToken");

    return "";  
  }
}

static esp_err_t send_photo()
{
  try
  {
    if (DebugMode)
        SendMessage("S:esp_err_t send_photo");
        
    if (Ready == true)
    {
      Ready = false;
              
      if (WiFi.status() == WL_CONNECTED && internet_connected == true)
      {
        Status = "IDE";
        
        esp_http_client_handle_t http_client;
        esp_http_client_config_t config_client = {0};
  
        if (HorusMode == 0)
          config_client.url = "http://server1.proyectohorus.com.ar/api/v2/functions/face/id?responseformat=pipe";
        else 
          config_client.url = "http://server1.proyectohorus.com.ar/api/v2/functions/object/detection?responseformat=pipe";
        
        config_client.event_handler = _http_event_handler;
        config_client.method = HTTP_METHOD_POST;
    
        http_client = esp_http_client_init(&config_client);
        
        esp_http_client_set_post_field(http_client, (const char *)fb->buf, fb->len);
        esp_http_client_set_header(http_client, "Content-Type", "image/jpg");
        esp_http_client_set_header(http_client, "Authorization",  APItoken.c_str());
        
        esp_err_t err = esp_http_client_perform(http_client);
    
        if (err != 0)
        {
          SendMessage("D:0|");
          SendMessage("S:ERR C-" + (String)err + "|");
          Status = "ERR C-" + (String)err;
        
          WiFi.disconnect(true);
          init_wifi();
        }
        
        esp_http_client_cleanup(http_client);
      }
      else
      {
        SendMessage("D:0|");
        SendMessage("S:ERR CONECTION|");
        Status = "ERR CONECTION";
      }
  
      Ready = true;    
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: send_photo");
  }
}

esp_err_t _http_event_handler(esp_http_client_event_t *evt)
{
  try
  {
    if (DebugMode)
        SendMessage("S:esp_err_t _http_event_handler");
        
    switch (evt->event_id) 
    {
      case HTTP_EVENT_ON_DATA:
        String Data = (char*)evt->data;
        String Content = Data.substring(0,evt->data_len);
        
        if (Content != "")
        {
          String ErrorCode = Split(Content,'|',0);
    
          // SI LA API RESPONDIO CON CODIGO DE ERROR 200 SIGNIFICA QUE TODO LLEGO OK.
          if (ErrorCode == "200")
          {
            String StatusCode = Split(Content,'|',1);          
            String UUIDDetection = Split(Content,'|',6);
            String UUIDProfile = Split(Content,'|',7);
            String Confidence = Split(Content,'|',8);
  
            // EL OBJETO DEBE TENER UN TAMAÑO MINIMO PARA SER ANALIZADO
            float ymin = Split(Content,'|',2).toFloat() * 100;
            float xmin = Split(Content,'|',3).toFloat() * 100;
            float ymax = Split(Content,'|',4).toFloat() * 100;
            float xmax = Split(Content,'|',5).toFloat() * 100;
  
            int Ancho = xmax - xmin;
            int Alto = ymax - ymin;
  
            if (Ancho >= MinBoxSize && Alto >= MinBoxSize)
            {
              if (UUIDDetection != "NOT FOUND" and UUIDDetection != "FAIL")
              {
                GetUserFromHorus(UUIDDetection);
              }
              else
              {
                if (UUIDDetection == "NOT FOUND")
                {
                  User = "No registrado";
                  Leyenda = "No registrado";
                  FlancoId = true;  
                  FaceFound = false;
                }
                else
                {
                  User = "";  
                  Leyenda = "";
                  FlancoId = false;  
                  FaceFound = false;
                }
              }    
            }
            else
            {
              SendMessage("D:0|");
              SendMessage("S:Acerquese a la camara|");
            } 
          }
          else
          {
            // SI NO LLEGO CON CODIGO 200 IMPLICA QUE ALGO OCURRIO Y PONEMOS EN CERO LA VARIABLE DE TOKEN PARA QUE INTENTE RECONECTAR
            if (ErrorCode.toInt() <= 500)
            {
              SendMessage("D:0|");
              SendMessage("S:ERR D-" + (String)ErrorCode + "|");
              Status = "ERR D-" + (String)ErrorCode;
          
              APItoken = "";
            }
          }
        }
        break;
    }
  
    Ready = true;
    
    return ESP_OK;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: _http_event_handler");
  }
}

void GetUserFromHorus(String UUIDDetection)
{
  try
  {
    if (DebugMode)
        SendMessage("S:GetUserFromHorus");
        
    String LeyendaJson = "";
    String ExternalURL = "";
    String ExternalVerb = "GET";
    
    if (HorusAuth == 1)
    {
      HTTPClient http;

      http.setTimeout(5000);
      http.begin("http://server1.proyectohorus.com.ar/api/v2/admin/accounts/users/profiles/detections=" + UUIDDetection + "/value?datalog=true"); 
      http.addHeader("Authorization", APItoken); 
      http.addHeader("Content-Type", "text/html"); 
      
      int httpCode = http.GET();
      
      if (httpCode > 0) 
      {
        String payload = http.getString();
        
        String input = String(Split(payload,'|',1));
        UUID = UUIDDetection;
        User = "";
        unsigned long TimeNow = getClock();
        
        if (input != "")
        {
          // EXTENDED MODE
          const size_t bufferSize = input.length() + 1024;
          
          DynamicJsonDocument doc(bufferSize);
          deserializeJson(doc, input);
          JsonObject obj = doc.as<JsonObject>();
        
          ExternalURL = obj["url"].as<String>();
          ExternalVerb = obj["verb"].as<String>();
          
          if (ExternalURL == "null" || ExternalURL == "")
          {
            User = obj["user"].as<String>();
            Id = obj["id"].as<String>();
            AccessFrom = obj["accessfrom"].as<int>();
            AccessTo = obj["accessto"].as<int>();
            AccessDay[0] = obj["day0"].as<bool>();
            AccessDay[1] = obj["day1"].as<bool>();
            AccessDay[2] = obj["day2"].as<bool>();
            AccessDay[3] = obj["day3"].as<bool>();
            AccessDay[4] = obj["day4"].as<bool>();
            AccessDay[5] = obj["day5"].as<bool>();
            AccessDay[6] = obj["day6"].as<bool>();
            UserActive = obj["active"].as<bool>();
            LeyendaJson = obj["leyenda"].as<String>();

            boolean Target = false;

            JsonArray targetArray = doc["target"];
            
            int arraySize = targetArray.size();

            if (arraySize == 0)
            {
              Target = true;  
            }
            else
            {
              for (int TargetIndex = 0; TargetIndex < arraySize; TargetIndex++)
              {              
                String localDevice = DeviceName;
                String targetDevice = targetArray[TargetIndex].as<String>();
                
                targetDevice.toLowerCase();
                targetDevice.trim();
                localDevice.toLowerCase();
                localDevice.trim();
                
                if (localDevice == targetDevice)
                {
                  Target = true; 
                  break;
                }
              }
            }
  
            if (UserActive == true && Target == true)
            {
              if (AccessDay[DayOfWeek(TimeNow)] == true)
              {
                if (MilitarHour(TimeNow) >= AccessFrom && MilitarHour(TimeNow) <= AccessTo)
                {
                  Leyenda = "";
                  FaceFound = true;
                }
                else
                {
                  Leyenda = "Fuera de horario";  
                  FaceFound = false;
                }
              }
              else
              {
                Leyenda = "Dia no habilitado";  
                FaceFound = false;
              }
            }
            else
            {
              if (LeyendaJson == "null")
                Leyenda = "Inactivo";
              else
                Leyenda = LeyendaJson;
                
                FaceFound = false;
            }
          
            FlancoId = true;
          }
          else
          {
            // SI USO CODIGICACION DE TERCEROS
            ExternalURL.replace("$DEVICE_NAME" , DeviceName);
            ExternalURL.replace("$USER_UUID" , UUID);
            ExternalURL.replace("$TIME", String(MilitarHour(TimeNow)));
            ExternalURL.replace("$CUSTOM", CustomVars);
            
            if (ExternalVerb == "null" || ExternalVerb == "")
              ExternalVerb = "GET";
              
            Http_Response Response = SendHTTPMessage(ExternalURL, ExternalVerb);
            
            if (Response.Status == 200)
            {
              deserializeJson(doc, Response.Data);
              JsonObject obj = doc.as<JsonObject>();
  
              User = obj["user"].as<String>();
              Id = obj["id"].as<String>();
              UserActive = obj["active"].as<bool>();
              LeyendaJson = obj["leyenda"].as<String>();
              
              if (User == "null" || User == "")
                User = "Desconocido";
  
              if (LeyendaJson == "null" || LeyendaJson == "")
                LeyendaJson = "Desconocido";
  
              if (Id == "null" || Id == "")
                Id = "0";
                          
              if (UserActive == true)
              {
                Leyenda = "";
                FaceFound = true;
              }
              else
              {
                Leyenda = LeyendaJson;  
                FaceFound = false;
              }
            }
            else
            {
              Id = "0";
              User = "Desconocido";
              Leyenda = "Server error";  
              FaceFound = false;
            }
  
            FlancoId = true;
          }
        }
      }
      else
      {
        User = "Desconocido";
        Leyenda = "Desconocido";
        FaceFound = false;
      }
      
      http.end();
    }
    else
    {                                        
      // SINGLE MODE
      HTTPClient http;

      http.setTimeout(5000);
      http.begin("http://server1.proyectohorus.com.ar/api/v2/admin/accounts/users/profiles/detections=" + UUIDDetection + "/name?datalog=true"); 
      http.addHeader("Authorization", APItoken); 
      http.addHeader("Content-Type", "text/html"); 
      
      int httpCode = http.GET();
      
      if (httpCode > 0) 
      {
        String payload = http.getString();
  
        Leyenda = "";
        User = String(Split(payload,'|',1));
        UUID = UUIDDetection;
        FaceFound = true;
      }
      else
      {
        User = "Desconocido";
        Leyenda = "Desconocido";
        FaceFound = false;
      }
  
      FlancoId = true;  
      
      http.end();
    }  
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: GetUserFromHorus");
  }
}

// CONECTA AL WIFI
bool init_wifi()
{
  try
  {    
    if (DebugMode)
        SendMessage("S:init_wifi");
        
    if (ssid != "")
    {
      int connAttempts = 0;
      if (internet_connected == false)
      {
        SendMessage("S:SSID " + String(ssid) + "|");
        Status = "SSID " + String(ssid);
      }
      else
      {
        SendMessage("S:FUERA DE SERVICIO|");
        Status = "RECONECTANDO";
      }
      
      delay(2000);
      
      WiFi.begin(string2char(ssid), string2char(password));
      
      while (WiFi.status() != WL_CONNECTED ) 
      {
            delay(1000);
            if (connAttempts > 20) 
                  return false;
                  
            connAttempts++;
      }

      ESP_ERROR_CHECK(esp_wifi_set_ps(WIFI_PS_NONE));
      ESP_ERROR_CHECK(esp_wifi_start());
    
      return true;
    }
    else
    {
      return false;
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: init_wifi");
    
    return false;  
  }
}

// LEO DESDE EL SERIAL
String ReadSerial()
{
  try
  {
    if (DebugMode)
        SendMessage("S:ReadSerial");
        
    String IncommingFromSerial = "";
    while (Serial.available() > 0) 
      IncommingFromSerial = IncommingFromSerial + String(char(Serial.read()));

    Serial.flush();
    
    return IncommingFromSerial;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: ReadSerial");

    return "";  
  }
}

// LEER EL PIROMETRO
float Pirometro(String IncommingFromSerial)
{
  float Temperature = 0.0;

  try
  {
    if (DebugMode)
        SendMessage("S:Pirometro");
        
    if (IncommingFromSerial != "")
    {  
      String Recived = "|";
      int Index = 0;
      
      while (Recived != "" && Index < 5)
      {
        Recived = Split(IncommingFromSerial,'|',Index);

        Recived.replace("|","");

        if (Split(String(Recived),':',0) == "T")
        {
          String Temp = Split(String(Recived),':',1);
          if (isNumeric(Temp)) 
          {
            Temperature = Temp.toFloat();

            break;
          }
        }
        
        Index++;
      }

      if (Temperature >= 45.00 || Temperature <= 25.00) 
        Temperature = 0.0; 
      }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: Pirometro");

  }

  return Temperature;
}

// LEER EL DISTANCIA
int mesureDistance(String IncommingFromSerial)
{
  int Distance = 0;

  try
  {
    if (DebugMode)
        SendMessage("S:mesureDistance");
        
    if (IncommingFromSerial != "")
    {  
      String Recived = "|";
      int Index = 0;
      
      while (Recived != "" && Index < 5)
      {
        Recived = Split(IncommingFromSerial,'|',Index);

        Recived.replace("|","");

        if (Split(String(Recived),':',0) == "M")
        {
          String Temp = Split(String(Recived),':',1);
          if (isNumeric(Temp)) 
          {
            Distance = Temp.toInt();

            break;
          }
        }
        Index++;
      }
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: mesureDistance");
  }

  return Distance;
  
}

// LEER EL TTL
String TTL(String IncommingFromSerial)
{
  String Response = "";

  try
  {
    if (DebugMode)
        SendMessage("S:TTL");
        
    if (IncommingFromSerial != "")
    {  
      String Recived = "|";
      int Index = 0;
      
      while (Recived != "" && Index < 5)
      {
        Recived = Split(IncommingFromSerial,'|',Index);

        if (Recived != "")
        {
          Recived.replace("|","");
  
          if (Split(String(Recived),':',0) == "P")
          {
              Response = Split(String(Recived),':',1);
  
              break;
          }
        }
        Index++;
      }
    }

    return Response;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: TTL");

    return "";  
  }
}

// OBTIENE UNA IMAGEN DE LA CAMARA
static esp_err_t take_photo()
{
  try
  {
    if (DebugMode)
        SendMessage("S:esp_err_t take_photo");
        
    fb = esp_camera_fb_get();

    if (fb)
    {
      if (fb->len > 0 && fb->len < 38400 && fb->format == PIXFORMAT_JPEG)
      {
        if (UltrasonicRequired == false)
        {
          uint8_t * out_buf = image_matrix->item;
          bool rest = fmt2rgb888(fb->buf, fb->len, fb->format, out_buf);
   
          if (!rest)
          {
            matrix3du_Ready = false;
            dl_matrix3du_free(image_matrix);
          }
          else
          {
            matrix3du_Ready = true;
  
            esp_camera_fb_return(fb);
          }
        }
        else
        {
          matrix3du_Ready = true;
          esp_camera_fb_return(fb);
        }
      }
      else
      {
        matrix3du_Ready = false;
        dl_matrix3du_free(image_matrix);
      }
    }
    else
    {      
      matrix3du_Ready = false;
      dl_matrix3du_free(image_matrix);
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: take_photo");
    
    matrix3du_Ready = false;
    dl_matrix3du_free(image_matrix);
  }
}

// DETECTA SI HAY ROSTROS EN LA IMAGEN Y A SU VEZ ESTAN CENTRADOS
boolean FaceDetection()
{
  try
  {
    if (DebugMode)
        SendMessage("S:FaceDetection");
        
    if (matrix3du_Ready)
    {
      net_boxes = face_detect(image_matrix, &mtmn_config);
      
      if (net_boxes)
      {
        box_array_t *boxes = net_boxes;
        
        int x = (int)boxes->box[0].box_p[0];
        int w = (int)boxes->box[0].box_p[2] - x + 1;
        int y = (int)boxes->box[0].box_p[1];
        int h = (int)boxes->box[0].box_p[3] - y + 1;
        
        float centroX = ((x + w) / 2);
        float centroY = ((y + h) / 2);
  
        int FaceWidth = (int)((WIDTH / 100)  * MinBoxSize);
        int FaceHeigth = (int)((HEIGHT / 100) * MinBoxSize);
    
        if ((w >= FaceWidth && h >= FaceHeigth) && (centroX >= 100 && centroX <= 200) && (centroY >= 50 && centroY <= 150)) 
        {
          return true;
        }
        else
        {
          SendMessage("D:0|");
          delay(100);
          SendMessage("S:Acerquese a la camara|");

          return false;
        }
      }
      else
      {
        return false;
      }
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: FeceDetection");

    return false;
  }
}

// RELOJ
unsigned long getClock()
{
  try
  {
    if (DebugMode)
        SendMessage("S:getClock");
        
    unsigned long unix_epoch = 0;
    
    if (WiFi.status() == WL_CONNECTED && internet_connected == true)
    {
      timeClient.update();
      unix_epoch = timeClient.getEpochTime();  
    }
    
    return unix_epoch; 
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: GetClock");
    
    return 0;  
  }
}
  
// DIA DE LA SEMANA
int DayOfWeek(unsigned long unix_epoch)
{
  try
  {
    if (DebugMode)
        SendMessage("S:DayOfWeek");
        
    if (unix_epoch > 0)
      return int(((unix_epoch / 86400L) + 3) % 7);
    else
      return 0; 
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: DayOfWeek");

    return 0;  
  }
}

// FORMATEAR HORA
String FormatHour(unsigned long unix_epoch)
{
  try
  {
    if (DebugMode)
        SendMessage("S:FormatHour");
        
    if (unix_epoch > 0)
    {
      char Time[ ] = "00.00";
      byte second_, minute_, hour_;
  
      minute_ = minute(unix_epoch);
      hour_   = hour(unix_epoch);
    
      Time[4]  = minute_ % 10 + 48;
      Time[3]  = minute_ / 10 + 48;
      Time[1]  = hour_   % 10 + 48;
      Time[0]  = hour_   / 10 + 48;  
    
      return String(Time);
    }
    else
      return "";
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: FormatHour");

    return "";  
  }
}

// FORMATEAR HORA
int MilitarHour(unsigned long unix_epoch)
{
  try
  {
    if (DebugMode)
        SendMessage("S:MilitarHour");
        
    if (unix_epoch > 0)
    {
      char Time[ ] = "0000";
      byte minute_, hour_;
  
      minute_ = minute(unix_epoch);
      hour_   = hour(unix_epoch);
    
      Time[3]  = minute_ % 10 + 48;
      Time[2]  = minute_ / 10 + 48;
      Time[1]  = hour_   % 10 + 48;
      Time[0]  = hour_   / 10 + 48;  
    
      return String(Time).toInt();
    }
    else
      return 0;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: MilitarHour");

    return 0;
  }
}

// MOTION DETECTION
bool capture_still() 
{
  try
  {
    if (DebugMode)
        SendMessage("S:capture_still");
        
    if (!fb)
      return false;
  
    if (matrix3du_Ready)
    {
      for (int y = 0; y < H; y++)
        for (int x = 0; x < W; x++)
          current_frame[y][x] = 0;
    
      int index = 0;
      for (uint32_t i = 0; i < WIDTH * HEIGHT; i++) 
      {
        const uint16_t x = i % WIDTH;
        const uint16_t y = floor(i / WIDTH);
        const uint8_t block_x = floor(x / BLOCK_SIZE);
        const uint8_t block_y = floor(y / BLOCK_SIZE);
    
        const uint8_t pixel = ((0.3 * image_matrix->item[index]) + (0.59 * image_matrix->item[index + 1]) + (0.11 * image_matrix->item[index + 2]));
        index = index + 3;
        
        const uint16_t current = current_frame[block_y][block_x];
    
        current_frame[block_y][block_x] += pixel;
      }
    
      for (int y = 0; y < H; y++)
        for (int x = 0; x < W; x++)
          current_frame[y][x] /= BLOCK_SIZE * BLOCK_SIZE;
  
      return true;
    }     
    else
    {
      return false;  
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: capture_still");

    return false;
  }
}

bool motion_detect() 
{
  try
  {
    if (DebugMode)
        SendMessage("S:motion_detect");
        
    uint16_t changes = 0;
    const uint16_t blocks = (WIDTH * HEIGHT) / (BLOCK_SIZE * BLOCK_SIZE);
  
    for (int y = 0; y < H; y++) 
    {
      for (int x = 0; x < W; x++) 
      {
        float current = current_frame[y][x];
        float prev = prev_frame[y][x];
        
        if (prev == 0.0)
          prev = 1.0;
          
        float delta = abs(current - prev) / prev;
        
        if (delta >= BLOCK_DIFF_THRESHOLD)
            changes += 1;
      }
    }
    
    return (1.0 * changes / blocks) > IMAGE_DIFF_THRESHOLD;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: motion_detect");

    return false;
  }
}

void update_frame() 
{
  try
  {
    if (DebugMode)
        SendMessage("S:update_frame");
        
    for (int y = 0; y < H; y++) 
    {
      for (int x = 0; x < W; x++) 
      {
        prev_frame[y][x] = current_frame[y][x];
      }
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: update_frame");
  }
}

bool MotionDetect() 
{
  if (DebugMode)
    SendMessage("S:MotionDetect");
        
  boolean Salida = false;

  if (millis() - last_motion_millis > 5000)
  { 
    if (capture_still()) 
    {
      if (motion_detect()) 
      {
        Salida = true;
        last_motion_millis = millis();
      }
      
      update_frame();
    }
  }
  else
  {
    update_frame();
    
    Salida = true;
  }
  
  return Salida;
}

// SETUP CLOCK
bool SetupClock()
{
  if (DebugMode)
    SendMessage("S:SetupClock");
        
  try
  {
    SendMessage("S:STR NTP|");

    if (WiFi.status() == WL_CONNECTED && internet_connected == true)
    {
      timeClient.begin();
  
      timeClient.setTimeOffset(ClockOffset);

      return true;
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: SetupClock");

    return false;  
  }
}

// SETUP ELEMENTS
bool SetupCam()
{
  try
  {
    SendMessage("S:STR CAMARA|");
        
    config.ledc_channel = LEDC_CHANNEL_0;
    config.ledc_timer = LEDC_TIMER_0;
    config.pin_d0 = Y2_GPIO_NUM;
    config.pin_d1 = Y3_GPIO_NUM;
    config.pin_d2 = Y4_GPIO_NUM;
    config.pin_d3 = Y5_GPIO_NUM;
    config.pin_d4 = Y6_GPIO_NUM;
    config.pin_d5 = Y7_GPIO_NUM;
    config.pin_d6 = Y8_GPIO_NUM;
    config.pin_d7 = Y9_GPIO_NUM;
    config.pin_xclk = XCLK_GPIO_NUM;
    config.pin_pclk = PCLK_GPIO_NUM;
    config.pin_vsync = VSYNC_GPIO_NUM;
    config.pin_href = HREF_GPIO_NUM;
    config.pin_sscb_sda = SIOD_GPIO_NUM;
    config.pin_sscb_scl = SIOC_GPIO_NUM;
    config.pin_pwdn = PWDN_GPIO_NUM;
    config.pin_reset = RESET_GPIO_NUM;
    config.xclk_freq_hz = 20000000;

    // PIXFORMAT_RGB565
    // PIXFORMAT_YUV422
    // PIXFORMAT_GRAYSCALE
    // PIXFORMAT_JPEG
    // PIXFORMAT_RGB888
    config.pixel_format = PIXFORMAT_JPEG;
    
    config.frame_size = FRAME_SIZE;
    config.jpeg_quality = 12;
    config.fb_count = 1;
  
    esp_err_t err = esp_camera_init(&config);
    
    if (err != ESP_OK) 
    {
          Serial.printf("CAMARA FAIL AT START, ERROR 0x%x", err);
          return false;
    }  

    SetCamara();
    
    return true;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: SetupCamara");

    return false;  
  }
}

bool SetCamara()
{
  try
  {
    if (DebugMode)
      SendMessage("S:SetCamara");
      
    sensor_t * s = esp_camera_sensor_get();
    
    s->set_brightness(s, brightness);
    s->set_contrast(s, contrast);
    s->set_saturation(s, saturation);
    s->set_special_effect(s, special_effect);
    s->set_whitebal(s, whitebal);
    s->set_awb_gain(s, awb_gain);
    s->set_wb_mode(s, wb_mode);
    s->set_exposure_ctrl(s, exposure_ctrl);
    s->set_aec2(s, aec2);
    s->set_ae_level(s, ae_level);
    s->set_aec_value(s, aec_value);
    s->set_gain_ctrl(s, gain_ctrl);
    s->set_agc_gain(s, agc_gain);
    s->set_gainceiling(s, (gainceiling_t)gainceiling);
    s->set_bpc(s, bpc);
    s->set_wpc(s, wpc);
    s->set_raw_gma(s, raw_gma);
    s->set_lenc(s, lenc);
    s->set_hmirror(s, hmirror);
    s->set_vflip(s, vflip);
    s->set_dcw(s, dcw);
    s->set_colorbar(s, colorbar);   
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: SetCamara");

    return false;  
  }
}

bool SetupDetector()
{
  try
  {
    SendMessage("S:STR DETECTOR|");

    last_capture_millis = millis();
    image_matrix = dl_matrix3du_alloc(1, WIDTH, HEIGHT, 3);  

    if(!image_matrix)
    {
        SendMessage("S:STR DETECTOR FAIL|");
        
        return false;
    }
    
    return true;
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: SetupDetector");

    return false;  
  }
}

bool SetupWifi()
{
  try
  {
    if (SDReady == true && IsResete())
    {
      SendMessage("S:WIFI WPS|");

      DeviceName = "CLOUD_SIGN_WPS";
      WPSReady = true;
      
      WiFi.onEvent(WiFiEvent);
      WiFi.mode(WIFI_MODE_STA);
    
      wpsInitConfig();
      esp_wifi_wps_enable(&configWPS);

      esp_wifi_wps_start(0);
      
      startCameraServer();
      ftpSrv.begin(FTPUser,FTPPass);
    }
    else
    {   
      SendMessage("S:STR WIFI|");

      if (init_wifi()) 
      { 
          SendMessage("S:IP " + ip2Str(WiFi.localIP()) + "|");
          
          startCameraServer();
          ftpSrv.begin(FTPUser,FTPPass);
          Function = "C";
          return true;
      }
      else
      {
        SendMessage("S:NO WIFI|");
  
        Function = "E";
        return false;
      }
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: SetupWifi");

    return false;  
  }
}

bool SetupSD()
{
  try
  {
    SendMessage("S:READING SD|");
    
    if (SD_MMC.begin("/sdcard",true) == true)
    {
      uint8_t cardType = SD_MMC.cardType();
      uint64_t cardSize = SD_MMC.cardSize() / (1024 * 1024);  
      
      return true;
    }
    else
    {
      return false;
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: SetupSD");

    return false; 
  }
}

// PRINCIPAL
void setup()
{
  WRITE_PERI_REG(RTC_CNTL_BROWN_OUT_REG, 0);

  Serial.begin(57600);
  
  SDReady = SetupSD();
  
  GetConfig(SD_MMC, "/config.json");
  
  CamaraReady = SetupCam();

  SetupDetector();

  internet_connected = SetupWifi();

  if (WPSReady == false)
  {
    checkForUpdates();
  
    SetupClock();
  
    // ENVIO CONFIGURACION A LA ARDUINO
    // F: INDICA SI ESTA EN MODO C=CLOUD O E=EDGE
    SendMessage("F:" + String(Function) + "|");
  
    // ENVIA EL MODO DE OPERACION EN NUBE DE HORUS 0=FACE ID, 1=OBJECT DETECTION
    SendMessage("M:" + String(HorusMode) + "|");
  
    // ENVIO CONFIGURACION DE VOLTAGE DE REFERENCIA 
    if (VoltageRef != 0)
      if (VoltageRef <= 5.0 && VoltageRef >= 1.0)
        SendMessage("X:" + String(VoltageRef) + "|");
  }

  IsResete(true);

  //WRITE_PERI_REG(RTC_CNTL_BROWN_OUT_REG, 1);
}

bool distanceVerificator(String SerialData)
{
  if (DebugMode)
    SendMessage("S:distanceVerificator");
      
  // SI SE DETECTA POR DISTANCIA
  if (mesureDistance(SerialData) <= UltrasonicMinDistance && mesureDistance(SerialData) > 0)
    return true;
  else
    return false;
}

void loop()
{
  try
  {
    // SI SE DESCONECTO PERO SE SUPONE QUE DEBE ESTAR CONECTADO RECONECTA SINO NO
    if (WiFi.status() != WL_CONNECTED && internet_connected == true)
    {
      WiFi.disconnect(true);
      init_wifi();
    }
    else
    {
      if (ConfigMode == true)
      {
        ftpSrv.handleFTP(); 
        delay(10);
        return;
      } 
    }

    if (WPSReady == true)
    {
      delay(10);
      return;
    }
    
    // SI EL TOKEN ESTA EN NULO SOLICITO UN TOKEN
    if (APItoken == "")
      if (WiFi.status() == WL_CONNECTED && internet_connected == true)
      {
        if (IDNotRequired == false && APIUser != "" && APIPassword != "" && APIprofileuuid != "")
        {
          SendMessage("S:CONNECTING|");
          Status = "CONNECTING";
        
          APItoken = GetToken(APIUser, APIPassword, APIprofileuuid);
  
          delay(10);
          return;
        }
      }
      
    // LEO EL SERIAL
    String SerialData = ReadSerial();

    // VARIABLE PROVENIENTE DEL TTL
    String FromTTLTemp = TTL(SerialData);
    if (FromTTLTemp != "")
    {
      FromTTL = FromTTLTemp;
      Last_TTLTimeOut = millis();
    }

    if (FromTTL != "")
      SendMessage("P:" + FromTTL + "|");

    if (millis() - last_capture_millis > capture_interval + WaitValue) 
    { 
      // ENVIO DATOS DE CABECERA
      // ENVIA UN CERO EN STATUS INDICANDO QUE NO HAY NADA QUE REPORTAR Y PONGO STATUS EN OK PARA LA INTERFAZ WEB
      SendMessage("S:0|");
      Status = "OK";
      
      // ENVIA LA HORA
      String Hour = FormatHour(getClock());
      if (LastHour != Hour)
      {
        SendMessage("L:" + FormatHour(getClock()) + "|");
        LastHour = Hour;

        // CONFIGURO SEGUN HORARIO
        CronConfig(SD_MMC, MilitarHour(getClock()));
      }
      
      // SETEA LA ULTIMA VEZ QUE SE ACCIONO, ESTO EVITA QUE SE GENERE UN LOOP MUY ACELERADO QUE CUELGUE LA ESP
      last_capture_millis = millis();
      // WAITVALUE INCREMENTA EL TIEMPO DE ESPERA ANTE UN EVENTO
      WaitValue = 0;
  
      // OBTENGO UNA IMAGEN
      take_photo();

      // SI ESTOY EN MODO DE IGNORAR TODO SALGO DEL PROCESO
      if (FaceNotRequired == true && TemNotRequired == true && IDNotRequired == true)
        return;

      // ENVIO DATO DE DETECCION SOLO SI ESTA ACTIVO EL FLANCO DE DETECCION EN EDGE SINO LO HAGO POR DETECCION DE MOVIMIENTO
      if (UltrasonicRequired == true)
      {
        if (distanceVerificator(SerialData) == true)
        {
          FlancoFace = true;
          SendMessage("D:1|");
          Last_TTLTimeOut = millis();
        }
        else
        {
          FlancoFace = false;
          SendMessage("D:0|");
        }
      }
      else
      {
        if (FaceNotRequired == false)
          FlancoFace = FaceDetection();
        else
          FlancoFace = MotionDetect();
      
        if (FlancoFace == true)
        {
          // SE DETECTE UN ROSTRO EN EDGE MANDO EL FLANCO DE DETECCION A LA ARDUINO PARA QUE PRENDA LOS LEDS
          SendMessage("D:1|");
          Last_TTLTimeOut = millis();
        }
        else
        {
          // SI SE DETECTA POR TEMPERATURA
          LastTemp = Temp;
          Temp = Pirometro(SerialData);
          
          if (Temp > (LastTemp * (1 + TempTrigger)) && Temp >= MinTemp && Temp <= MaxTemp)
          {
            // SE DETECTA MOVIMIENTO POR CAMBIO DE TEMPERATURA MANDO EL FLANCO DE DETECCION A LA ARDUINO PARA QUE PRENDA LOS LEDS
            FlancoFace = true;
            
            SendMessage("D:1|");
            Last_TTLTimeOut = millis();
          }
          else
          {
              FlancoFace = false;
              SendMessage("D:0|");
          }
        }
      }
      // INDICO A LA INTERFAZ WEB QUE NO HAY ACCESO HASTA QUE SE COMFIRME, SOLO SE USA PARA ESO Y NADA MAS
      Acceso = false;

      // VERIFICO TEMPERATURA 
      if (FlancoFace == true)
      {
        float TermMesure = 0.0;
        Temp = 0.0;
        for (int muestras = 0; muestras <= 3; muestras++)
        {
          delay(100);
          TermMesure = Pirometro(ReadSerial());
          if (TermMesure > Temp)
            Temp = TermMesure;
        } 

        // AJUSTO EL VALOR DE MEDICION SEGUN LA ESCALA NORMALIZADA SI ES QUE LA HAY
        Temp = Temp + Offset;
        Temp = Scale(SD_MMC, "/scale.json", Temp);


        // SI LA TEMPERATURA ES BAJA LO MUESTRA EN PANTALLA Y SALE DEL CICLO
        if (Temp <= MinTemp)
        {
            SendMessage("D:0|");
            SendMessage("S:TEMPERATURA BAJA|");

            return;
        }
      }
   
      if (Temp != 0.0 && TemNotRequired == false)
        FlancoTemp = true;
      else
        FlancoTemp = TemNotRequired;

      // VERIFICO FACE ID    
      if (IDNotRequired == false)
      {
        if (FlancoId == false)
        {
          // SI HAY UNA ENTRADA EN TTL CONSULTO AL ENDPOINT DEL TTL SINO AL DE FACE ID
          if (FromTTL != "")
          {
            String UUIDDetection = "";
            
            if (TTL_URL != "")
            {
              // SI USO CODIGICACION DE TERCEROS
              String TTL_URL_TEMP = TTL_URL;
              TTL_URL_TEMP.replace("$TTL" , FromTTL);
              TTL_URL_TEMP.replace("$DEVICE_NAME" , DeviceName);
              TTL_URL_TEMP.replace("$TIME", String(MilitarHour(getClock())));
              TTL_URL_TEMP.replace("$CUSTOM", CustomVars);
              
              Http_Response Response = SendHTTPMessage(TTL_URL_TEMP, TTL_VERB);
              
              if (Response.Status == 200)
              {
                String input = Response.Data;
                
                if (input != "")
                {
                  const size_t bufferSize = input.length() + 1024;
                  
                  DynamicJsonDocument doc(bufferSize);
                  deserializeJson(doc, input);
                  JsonObject obj = doc.as<JsonObject>();
                  
                  String LeyendaJson = "";
                  
                  UUIDDetection = obj["uuid"].as<String>();
                    
                  if (UUIDDetection == "null")
                    UUIDDetection = "";

                  if (UUIDDetection != "")
                  {
                      GetUserFromHorus(UUIDDetection);
                  }
                  else
                  {
                    User = obj["user"].as<String>();
                    Id = obj["id"].as<String>();
                    UserActive = obj["active"].as<bool>();
                    LeyendaJson = obj["leyenda"].as<String>();

                    if (UserActive == true)
                    {
                      if (User == "null" || User == "")
                        User = "**********";
  
                      if (Id == "null" || Id == "")
                        Id = "0";

                      FaceFound = true;
                    }
                    else
                    {
                      if (LeyendaJson == "null")
                        LeyendaJson = "";

                      User = "";
                      Id = "0";
                      Leyenda = LeyendaJson;  
                      FaceFound = false;
                    }
                  }
                }
                else
                {
                  UUIDDetection = "";
                  User = "Desconocido";
                  Leyenda = "Desconocido";
                  FaceFound = false;
                }
              }
              else
              {
                UUIDDetection = "";
                User = "Desconocido";
                Leyenda = "Server error";
                FaceFound = false;
              }

              FlancoId = true;
            }
            else
            {  
              // SI USO CODIFICACION HORUS
              UUIDDetection = FromTTL;

              if (UUIDDetection != "")
                GetUserFromHorus(UUIDDetection);
            }
          }
          else
          {
            // SI ESTOY EN MODO FACE DETECTION SOLO MANDO IMAGEN SI ES QUE HAY UN ROSTRO EN CAMARA
            if (FlancoFace == true)
              send_photo();
          }
        }
      }
      else
      {
        FlancoId = true;
      }
        
      // ENVIO ACTIVACION O NO
      if (FlancoFace == true && FlancoTemp == true && FlancoId == true)      
      {  
        last_motion_millis = last_motion_millis - 5000;
        
        // ENVIO DEL USUARIO DETECTADO
        if (User == "")
          User = "0"; 
          
        SendMessage("U:" + User + "|");  

        // ENVIO DATOS DE TEMPERATURA
        SendMessage("T:" + String(Temp) + "|");
        
        if ((Temp >= MinTemp && Temp <= MaxTemp) || TemNotRequired == true)
        {
          if (FaceFound == true)
          {
            if (Temp < AlertTemp || TemNotRequired == true)
            {
              Acceso = true;

              String OK_URL_TEMP = OK_URL;
              OK_URL_TEMP.replace("$USER_NAME" , User);
              OK_URL_TEMP.replace("$TEMPERATURE" , String(Temp));
              OK_URL_TEMP.replace("$DEVICE_NAME" , DeviceName);
              OK_URL_TEMP.replace("$USER_UUID" , UUID);
              OK_URL_TEMP.replace("$TTL" , FromTTL);
              OK_URL_TEMP.replace("$TIME", String(MilitarHour(getClock())));
              OK_URL_TEMP.replace("$CUSTOM", CustomVars);
              OK_URL_TEMP.replace(" ","%20");

              SendHTTPMessage(OK_URL_TEMP, OK_VERB);
              
              // ENVIO LEYENDA
              SendMessage("L:" + Leyenda + "|");
              Leyenda = "";
  
              // ENVIO ACCESO
              SendMessage("R:2:" + String(ActionWait) + "|");
              WaitValue = ActionWait;
            }
            else
            { 
              String FAIL_TMP_URL_TEMP = FAIL_TMP_URL;
              FAIL_TMP_URL_TEMP.replace("$USER_NAME" , User);
              FAIL_TMP_URL_TEMP.replace("$TEMPERATURE" , String(Temp));
              FAIL_TMP_URL_TEMP.replace("$DEVICE_NAME" , DeviceName);
              FAIL_TMP_URL_TEMP.replace("$USER_UUID" , UUID);
              FAIL_TMP_URL_TEMP.replace("$TTL" , FromTTL);
              FAIL_TMP_URL_TEMP.replace("$TIME", String(MilitarHour(getClock())));
              FAIL_TMP_URL_TEMP.replace("$CUSTOM", CustomVars);
              FAIL_TMP_URL_TEMP.replace(" ","%20");

              SendHTTPMessage(FAIL_TMP_URL_TEMP, FAIL_TMP_VERB);
              
              // ENVIO LEYENDA
              SendMessage("L:" + Leyenda + "|");
              Leyenda = "";
  
              // ENVIO ACCESO
              SendMessage("R:3:" + String(ActionWait) + "|");
              WaitValue = ActionWait;
            }
          }
          else
          {
            String FAIL_UNKNOW_URL_TEMP = FAIL_UNKNOW_URL;
            FAIL_UNKNOW_URL_TEMP.replace("$USER_NAME" , User);
            FAIL_UNKNOW_URL_TEMP.replace("$TEMPERATURE" , String(Temp));
            FAIL_UNKNOW_URL_TEMP.replace("$DEVICE_NAME" , DeviceName);
            FAIL_UNKNOW_URL_TEMP.replace("$USER_UUID" , UUID);
            FAIL_UNKNOW_URL_TEMP.replace("$TTL" , FromTTL);
            FAIL_UNKNOW_URL_TEMP.replace("$TIME", String(MilitarHour(getClock())));
            FAIL_UNKNOW_URL_TEMP.replace("$CUSTOM", CustomVars);
            FAIL_UNKNOW_URL_TEMP.replace(" ","%20");

            SendHTTPMessage(FAIL_UNKNOW_URL_TEMP, FAIL_UNKNOW_VERB);
            
            // ENVIO LEYENDA
              SendMessage("L:" + Leyenda + "|");
              Leyenda = "";
  
              // ENVIO ACCESO
            SendMessage("R:4:" + String(ActionWait) + "|");
            WaitValue = ActionWait;
          }
        }

        LastHour = "";
        Leyenda = "";
        User = "";
        FromTTL = "";
        FlancoFace = false;
        FlancoTemp = TemNotRequired;
        FlancoId = IDNotRequired;
        FaceFound = IDNotRequired;
      }
      else
      {
        if (millis() > Last_TTLTimeOut + TTLTimeOut)
        {
          FromTTL = "";
          User = "";
          Leyenda = "";
          FlancoId = IDNotRequired;
        }

        delay(1);
      }
    }
    else
    {
      delay(10);  
    }
  }
  catch(const std::exception& ex) 
  {
    SendMessage("S:FAIL ON: Loop");
  }
}
