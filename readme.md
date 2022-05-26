# ESC Pos Server

A simple Rest API server for printing receipt on ESC/POS printer.

# How to use
1. Send HTTP Post Request to `/print`.
```
{
    "products": [
        {
            "name": "Pasir Merah",
            "qty": 100,
            "unit": "pcs",
            "price": 3000,
            "discount": 500
        }
    ],
    "cash": 300000,
    "meta": {
        "store_name": "UD. TOKO BANGUNAN",
        "store_address": "JL. Satu Dua Tiga",
        "phone": "0852325435",
        "trx_id": "01G2HP6TAD5J46TQXX7Q0CFDK2"
    }
}
```

# Configuration
1. Set the printer name on `appsettings.json`