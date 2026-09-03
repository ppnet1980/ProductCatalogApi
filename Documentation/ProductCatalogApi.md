# ProductCatalogApi

Krótki opis

Product Catalog API (kontroler ProductsController) — proste REST API do zarządzania katalogiem produktów.
Implementacja oparta na podstawowym ASP.NET Core (ApiController). Dane przechowywane są w pamięci aplikacji (statyczna lista `Products`).

Ważne informacje

- Repozytorium: ppnet1980/ProductCatalogApi
- Kontroler główny: ProductsController (ścieżka bazowa: /products)
- Format: JSON
- Przechowywanie: in-memory (statyczna lista w kontrolerze)

Lista metod (endpoints)

1) GET /products
- Opis: Pobiera listę wszystkich produktów.
- Route: [GET] /products
- Request body: brak
- Response:
  - 200 OK — lista produktów w formacie JSON
- Przykład odpowiedzi (200 OK):
```
[
  {
    "id": 1,
    "name": "Laptop Pro 14",
    "category": "Electronics",
    "price": 6499.00,
    "isActive": true
  },
  {
    "id": 2,
    "name": "Office Chair Comfort",
    "category": "Furniture",
    "price": 899.00,
    "isActive": true
  },
  {
    "id": 3,
    "name": "Noise Cancelling Headphones",
    "category": "Electronics",
    "price": 1299.00,
    "isActive": false
  }
]
```

2) GET /products/{id}
- Opis: Pobiera produkt o podanym ID.
- Route: [GET] /products/{id}
- Parametry ścieżki:
  - id (int) — identyfikator produktu
- Response:
  - 200 OK — zwraca obiekt Product (JSON)
  - 404 Not Found — gdy produkt o podanym ID nie istnieje
- Przykład odpowiedzi (200 OK):
```
{
  "id": 1,
  "name": "Laptop Pro 14",
  "category": "Electronics",
  "price": 6499.00,
  "isActive": true
}
```
- Przykład odpowiedzi (404 Not Found):
```
404 Not Found
"Nie znaleziono produktu o ID = {id}."
```

3) POST /products
- Opis: Tworzy nowy produkt.
- Route: [POST] /products
- Request body: JSON z właściwościami produktu (bez pola Id — zostanie nadane automatycznie)
  - name (string)
  - category (string)
  - price (decimal)
  - isActive (bool)
- Response:
  - 201 Created — zwraca utworzony obiekt oraz nagłówek Location wskazujący GET /products/{id}
- Przykład request:
```
POST /products
Content-Type: application/json

{
  "name": "New Product",
  "category": "Gadgets",
  "price": 199.99,
  "isActive": true
}
```
- Przykład odpowiedzi (201 Created):
```
201 Created
Location: /products/4

{
  "id": 4,
  "name": "New Product",
  "category": "Gadgets",
  "price": 199.99,
  "isActive": true
}
```

4) PATCH /products/{id}/status
- Opis: Aktualizuje pole isActive (status) produktu o podanym ID.
- Route: [PATCH] /products/{id}/status
- Parametry ścieżki:
  - id (int) — identyfikator produktu
- Request body: prosty JSON/bool lub surowa wartość logiczna oczekiwana przez model binder (w implementacji metoda przyjmuje [FromBody] bool isActive)
  - Przykład request body: true
- Response:
  - 200 OK — zwraca zaktualizowany produkt
  - 404 Not Found — gdy produkt o podanym ID nie istnieje
- Przykład request i odpowiedzi:
```
PATCH /products/2/status
Content-Type: application/json

true

---
200 OK
{
  "id": 2,
  "name": "Office Chair Comfort",
  "category": "Furniture",
  "price": 899.00,
  "isActive": true
}
```

Model danych

Product
- Id (int) — identyfikator produktu
- Name (string) — nazwa produktu (domyślnie string.Empty w modelu)
- Category (string) — kategoria (domyślnie string.Empty)
- Price (decimal) — cena
- IsActive (bool) — flaga aktywności

Przykładowa definicja (C#)

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}

Uwagi implementacyjne

- Dane są przechowywane w statycznej liście `Products` w kontrolerze (po restarcie aplikacji dane wracają do wartości początkowych).
- Tworzenie produktu (POST) ustawia Id jako (max(Id) + 1) lub 1, jeśli lista jest pusta.
- PATCH /products/{id}/status przyjmuje tylko aktualizację pola `IsActive`.

Przykłady użycia (curl)

- GET lista produktów:
  curl -X GET "http://<host>:<port>/products"

- GET produkt o id=1:
  curl -X GET "http://<host>:<port>/products/1"

- POST nowy produkt:
  curl -X POST "http://<host>:<port>/products" -H "Content-Type: application/json" -d '{"name":"New","category":"G","price":9.99,"isActive":true}'

- PATCH aktualizacja statusu:
  curl -X PATCH "http://<host>:<port>/products/2/status" -H "Content-Type: application/json" -d 'false'

Historia zmian

- 2026-09-03 — PR #9 (branch: ppnet1980-patch-8) — zdarzenie: closed. Zaktualizowano dokumentację: ujednolicono opis endpointów i przykładów odpowiedzi zgodnie z kodem w kontrolerze ProductsController.
- 2026-09-03 — Poprzednie wpisy i pełna historia znajdują się w repozytorium (zachowano główne sekcje dokumentu).

Linki i dodatkowe pliki

- Kod kontrolera: Controllers/ProductsController.cs
- Model: Models/Product.cs
- Wymagania dokumentacji: Documentation/DocumentationRequirements.md

---

Jeżeli potrzebujesz rozszerzyć dokumentację (np. dodać szczegółowy opis błędów, schematy JSON/Swagger lub przykłady dla wszystkich kodów statusu), daj znać.