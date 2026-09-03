# ProductCatalogApi

Wersja API: v1

Krótki opis

ProductCatalogApi to proste API katalogu produktów (wersja v1). Umożliwia listowanie produktów, pobieranie pojedynczego produktu, tworzenie nowego produktu oraz aktualizację statusu (aktywne/nieaktywne). Dane przechowywane są w pamięci (statyczna lista) — API służy jako przykład/prototyp.

Base URL

Domyślny URL aplikacji zależy od konfiguracji hosta (np. http://{HOST}). Kontroler jest mapowany na ścieżkę /products.

Autoryzacja

Brak. API nie wymaga uwierzytelniania w dostarczonej implementacji.

Zależności

- .NET 6 / ASP.NET Core (projekt ProductCatalogApi.V1)

Model

Model: Product (Models/Product.cs)

- id (int) — identyfikator produktu
- name (string) — nazwa produktu
- category (string) — kategoria produktu
- price (decimal) — cena produktu
- isActive (bool) — flaga aktywności produktu

Uwaga: Model ma proste pola odpowiadające implementacji w kodzie.

Lista endpointów (aktualne — zgodne z kodem źródłowym Controllers/ProductsController.cs)

1) GET /products
- Opis: Pobierz listę produktów
- Metoda: GET
- Parametry: brak
- Odpowiedzi:
  - 200 OK — lista produktów (application/json)

Przykład curl:

curl -s -X GET "http://{HOST}/products"

2) GET /products/{id}
- Opis: Pobierz produkt po id
- Metoda: GET
- Parametry:
  - id — identyfikator produktu (int) w ścieżce
- Odpowiedzi:
  - 200 OK — produkt (application/json)
  - 404 Not Found — produkt z podanym id nie istnieje

Przykład curl:

curl -s -X GET "http://{HOST}/products/1"

3) POST /products
- Opis: Dodaj nowy produkt
- Metoda: POST
- Body: application/json (obiekt Product bez pola id)
  Przykład body:
  {
    "name": "Nowy Produkt",
    "category": "Kategoria",
    "price": 123.45,
    "isActive": true
  }
- Odpowiedzi:
  - 201 Created — utworzony produkt (z polami, w tym wygenerowanym id) oraz nagłówek Location wskazujący GET /products/{id}

Przykład curl:

curl -s -X POST "http://{HOST}/products" -H "Content-Type: application/json" -d '{"name":"Nowy Produkt","category":"Kategoria","price":123.45,"isActive":true}'

Uwagi do tworzenia:
- Id jest generowane na podstawie największego istniejącego id + 1. Jeśli lista jest pusta, id = 1.

4) PATCH /products/{id}/status
- Opis: Aktualizuj flagę isActive (status) produktu o podanym id
- Metoda: PATCH
- Ścieżka: /products/{id}/status
- Body: application/json (wartość bool) — w kontrolerze metoda przyjmuje [FromBody] bool isActive
  Przykład body: true
- Odpowiedzi:
  - 200 OK — zwraca zaktualizowany obiekt produktu (application/json)
  - 404 Not Found — jeśli produkt o podanym id nie istnieje (z komunikatem: "Nie znaleziono produktu o ID = {id}.")

Przykład curl:

curl -s -X PATCH "http://{HOST}/products/1/status" -H "Content-Type: application/json" -d 'false'

Uwagi implementacyjne

- Dane są przechowywane w pamięci w statycznej liście Products w kontrolerze (Controllers/ProductsController.cs). Oznacza to, że dane nie są trwałe i zrestartowanie aplikacji zresetuje listę do wartości domyślnych z kodu.
- Brak endpointu DELETE — usuwanie nie jest zaimplementowane.
- Implementacja Create używa CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct) — czyli zwraca 201 Created z lokalizacją nowego zasobu.

Przykładowe dane startowe (statyczna lista w kodzie)

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

Historia zmian

- 2026-09-03 — Zaktualizowano dokumentację na podstawie kodu źródłowego po zdarzeniu PR #8 (branch ppnet1980-patch-7 zamknięty). Potwierdzono dostępne endpointy i opisano istniejący PATCH /products/{id}/status. Dodano uwagi o przechowywaniu danych w pamięci i o sposobie generowania id.
- 2026-09-03 — Wersja początkowa dokumentu (wcześniejsze wpisy zachowane).

Uwagi końcowe

Dokumentacja została zsynchronizowana z aktualnym kodem w repozytorium. Nie wprowadzono nowych endpointów ani nie wymyślono brakujących metod — opisuje wyłącznie to, co znajduje się w kontrolerze ProductsController.cs.
