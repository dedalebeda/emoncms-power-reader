# Description

.Net MicroFramework code for measuring voltage and current and posting values to an [EmonCMS](http://emoncms.org) site. Software is tested on Netduino Plus 2 with MF 4.3.

# Other hardware requirements

Based on your setup, you need two resistors for voltage divider and a current sensor. I'm using one based on **ACS712** using Hall effect.

## Sample wiring

For 300 VDC and 10A, I will use ACS712 30A chip and resistors 470k and 4k7 ohms to keep output voltage safely under 3.3 volts. 

Both resistors are in serie between plus and minus pole. Their connection is connected to analog A1 port of Netduino.

ACS712 is powered by 5V from Netduino, OUT port is connected to A2 Netduino's port.

# Code compilation

You need a Visual Studio 2015 with Micro Framework extensions as described on this [forum](http://forums.netduino.com/index.php?/topic/11816-netduino-plus-2-firmware-v432-update-1/). There is also link for used Netduino firmware.