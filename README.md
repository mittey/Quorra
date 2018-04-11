# Quorra
A smart assistant at Sopra Steria Saint Petersburg


# Some steps to run Quorra 

(This will be updated to a more detail instuction)

1. ngrok http 8443 -host-header="localhost:8443"
2. https://api.telegram.org/bot[BOT_KEY]/setWebhook?url=[NGROK_ADDRESS]/api/update