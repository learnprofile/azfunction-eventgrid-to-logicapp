# Azure Function - Eventgrid to LogicApp

Part of Azure AppDev Challenge

IoT Simulator https://github.com/gidavies/AlarmsIOTSimulator

Docker command:

```
docker run -e AlarmTopic='https://xxx.northeurope-1.eventgrid.azure.net/api/events'  `
    -e  AlarmKey='AsoKyMOHkA/2EVWUKN8cQ28sUHrrnXkUPtJ/9i7aE2g='   `
    -e AlarmImageRoot='https://alarmimages.blob.core.windows.net/alarmimages/'  `
    gdavi/alarms-iot-simulator
```
