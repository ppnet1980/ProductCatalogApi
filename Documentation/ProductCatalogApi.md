# ProductCatalogApi

Wersja API: v1

Krótski opis

ProductCatalogApi to proste API katalogu produktów (wersja v1). Umożliwia listowanie produktów, pobieranie pojedynczego produktu oraz tworzenie nowych pozycji. Działania są wykonywane w pamięci (statyczna lista w kontrolerze) — dane nie są zapisywane do bazy.

Base URL

Domyślny URL aplikacji zależy od konfiguracji hosta (np. http://{HOST}). Kontroler jest mapowany na ścieżkę /products.

Autoryzacja

Brak — API nie wymaga uwierzytelnienia. (Zgodnie z aktualną implementacją kontrolera w repozytorium.)

Zakres funkcji

ProductCatalogApi v1 pozwala na:
- listowanie produktów (GET /products)
- pobieranie pojedynczego produktu (GET /products/{id})
- tworzenie nowego produktu (POST /products)

Modele

Model: Product
- id (int) — identyfikator produktu
- name (string) — nazwa produktu
- category (string) — kategoria produktu
- price (decimal) — cena produktu
- isActive (bool) — flaga aktywności produktu

Przykładowe JSON (lista / przykładowe elementy):

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

Endpointy

1) Pobierz listę produktów
- Metoda: GET
- Ścieżka: /products
- Parametry: brak
- Odpowiedź: 200 OK — tablica obiektów Product

2) Pobierz produkt po id
- Metoda: GET
- Ścieżka: /products/{id}
- Parametry: id (int) — identyfikator produktu
- Odpowiedź:
  - 200 OK — zwraca obiekt Product gdy produkt istnieje
  - 404 Not Found — gdy produkt o podanym id nie istnieje

3) Utwórz nowy produkt
- Metoda: POST
- Ścieżka: /products
- Body: JSON (Product) — obiekt produktu bez pola id (id jest nadawane automatycznie przez kontroler)
- Odpowiedź:
  - 201 Created — lokalizacja nowo utworzonego zasobu /products/{id} (nagłówek Location), odpowiedź zawiera utworzony obiekt Product

Brak endpointu DELETE

W aktualnej implementacji kontrolera ProductsController (Controllers/ProductsController.cs) nie ma metody oznaczonej atrybutem [HttpDelete]. Oznacza to, że API nie obsługuje usuwania produktów (np. DELETE /products/{id}) — próba wykonania takiego żądania prawdopodobnie zwróci 404 Not Found lub 405 Method Not Allowed, w zależności od konfiguracji serwera/middleware.

Przykłady cURL

1) Pobierz listę produktów

curl -sS -X GET "http://{HOST}/products"

2) Pobierz produkt po id=1

curl -sS -X GET "http://{HOST}/products/1"

3) Utwórz nowy produkt

curl -sS -X POST "http://{HOST}/products" -H "Content-Type: application/json" -d '{"name":"Nowy Produkt","category":"Kategoria","price":123.45,"isActive":true}'

Uwagi implementacyjne i ograniczenia

- Dane są przechowywane w pamięci w postaci statycznej listy Products w kontrolerze ProductCatalogApi.V1.Controllers.ProductsController. Po restarcie aplikacji lista wraca do wartości początkowych.
- Tworzenie produktu (POST) przygotowuje nowy obiekt Product i nadaje mu Id: jeżeli lista jest pusta Id=1, w przeciwnym razie Id = Max(existing Id) + 1. Następnie nowy produkt jest dodawany do listy i zwracany z kodem 201 Created (CreatedAtAction wskazującym na GET /products/{id}).
- Brak dodatkowej walidacji pól (np. sprawdzenia price >= 0) — dane z body są przepuszczane bez rozbudowanej walidacji.
- Brak paginacji, filtrowania i sortowania.

Kod źródłowy (lokacje)

- Kontroler: Controllers/ProductsController.cs
- Model danych: Models/Product.cs
- Wymagania dokumentacji: Documentation/DocumentationRequirements.md

Historia zmian

- 2026-09-03 — Aktualizacja: zaktualizowano dokumentację zgodnie z aktualnym kodem źródłowym; usunięto wzmiankę o istniejącym endpointzie DELETE i wyjaśniono, że usuwanie nie jest zaimplementowane.
- 2026-09-03 — Wersja początkowa dokumentu.