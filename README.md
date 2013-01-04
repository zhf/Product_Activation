Product_Activation
==================

Easy Offline Product Activation

## Requirement

- Microsoft Windows Vista or later
- .Net Framework 3.5 or later

## What's Implemented

- Key-gen
- Computing MachineID
- Creating signature (license) based on MachineID using private key
- Verifying signature using public key
- Helper functions for transferring code offline

## Security

- Using 256-bit ECDSA, which is an overkill for such apps (I believe Micorosft is using some 64-bit Elliptic Curve Signature Algorithm in Windows and Office Activation), while should be quite safe in the next decade.
- If you are port the code to another platform, consider 112-bit key to shorten the signature
- Product Activation Mechanism does not protected your software against cracking

## Using the Demo

- Use `ECC_Key_Gen.exe` to create keys
- Use `PA_Public.exe` to get your MachineID
- use `PA_Private.exe <MachineID>` to create signature for client
- Use `PA_Public.exe <Signature_in_Base64>` to verify signature
