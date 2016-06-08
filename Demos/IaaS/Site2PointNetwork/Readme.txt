Create the Cert to upload to the Portal
when using ARM uplaod the Cert using Resource Template

Run Cert2Base64.exe to get the Base64 String for the ARM Tenmplate

-- Create Root Cert using VS command prompt
makecert -sky exchange -r -n "CN=Azure-VNetPortal-Root-Cert" -pe -a sha1 -len 2048 -ss My
-- Create Client Cert using VS command prompt (înstall on every VPN Client)
makecert.exe -n "CN=Azure-VNetPortal-Client-Cert" -pe -sky exchange -m 96 -ss My -in "Azure-VNetPortal-Root-Cert" -is my -a sha1
--Export Root Cert 
Open MMC -> Add SnapIn -> My Cert -> Export Root Cert without key rest default
--Export Client Cert as PFX
Open MMC -> Add SnapIn -> My Cert -> Export Client Cert with key rest default password:trivadis
