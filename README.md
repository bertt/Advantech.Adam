# Advantech.Adam

Sample code for reading/writing to Advantech Adam 6017 (Analog) and Advantech Adam 6060 (Digital).

Before using this code install the drivers, see https://www.advantech.com/search/?q=ADAM-6060&st=support&sst=Driver&from=support

![image](https://user-images.githubusercontent.com/538812/153036189-52478964-1f50-4e63-be6a-6f011a36db1e.png)

Sample output in console:

```
Start Adam6060 demo

Adam6060 connected: True

Adam606 firmware: 5.04 B01

Adam6060 Input channels: False, False, False, False, False, False

Adam6060 Output channels:True, False, False, True, False, True

End Adam6060 demo
-----------------------------------------------------------------------------

Start Adam6017 demo

Adam6017 connected: True

Adam6017 firmware: 4.17 B01

Ch-0: 4.000 mA

Ch-1: 0.002 V

Ch-2: 0.001 V

Ch-3: 0.002 V

Ch-4: 0.003 V

Ch-5: 0.001 V

Ch-6: 0.003 V

Ch-7: 0.001 V

End Adam6017 demo
```
