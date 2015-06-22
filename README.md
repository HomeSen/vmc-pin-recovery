## VodafoneDecryptor

This is a simple tool, born out of necessity and quickly hacked together, to retrieve SIM-card PINs that got saved in <em>Vodafone's Mobile Broadband</em> client. It reads the encrypted PINs from a user's application data folder, and decrypts them using the encryption/decryption key that is stored inside the Windows user account's key store.

In order to successfully retrieve the saved PINs, the program must be run under the respective user account that saved it ;)


### Usage:
- `VodafoneDecryptor.exe`
Read and decrypt PINs from user's <em>Vodafone Mobile Broadband</em> config
- `VodafoneDecryptor.exe <path to config file>`
Read and decrypt PINs from <em>Vodafone Mobile Broadband</em> config located at the given path

![Sample output](https://github.com/HomeSen/vmc-pin-recovery/blob/master/sample-output.png "Sample output")
