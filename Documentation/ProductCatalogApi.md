# ProductCatalogApi

Wersja API: v1

Krótski opis

ProductCatalogApi to proste API katalogu produktów (wersja v1). Umożliwia listowanie produktów, pobieranie pojedynczego produktu oraz tworzenie nowych pozycji. Działania są wykonywane w pamięci (statyczna lista w kontrolerze) — dane nie są zapisywane do bazy.

Base URL

Domyślny URL aplikacji zależy od konfiguracji hosta (np. http://{HOST}). Kontroler udostępnia mapowany na ścieżkę /products.

Autoryzacja

Brak — API nie wymaga uwierzytelnienia. (Zgodnie z aktualną implementacją kontrolera w repozytorium.)

Zakres funkcji

ProductCatalogApi v1 pozwala na:
- listowanie produktów (GET)
- pobieranie pojedynczego produktu (GET /{id})
- tworzenie nowego produktu (POST)

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
- Body: JSON (Product) — obiekt produktu bez pola id (id jest nadawane automatycznie)
- Odpowiedź:
  - 201 Created — lokalizacja nowo utworzonego zasobu /products/{id}, odpowiedź zawiera utworzony obiekt Product

Uwaga — brak endpointu DELETE

W aktualnej implementacji kontrolera (ProductsController) nie ma zaimplementowanego endpointu HTTP DELETE (np. DELETE /products/{id}). Oznacza to, że API nie obsługuje usuwania produktów. Jeśli chcesz, mogę przygotować propozycję implementacji metody DELETE (fragment kodu i zachowania), jednak taki kod nie zostanie dodany do dokumentacji oficjalnej dopóki nie pojawi się w repozytorium jako część implementacji.

Przykłady cURL

1) Pobierz listę produktów

curl -sS -X GET "http://{HOST}/products"

2) Pobierz produkt po id=1

curl -sS -X GET "http://{HOST}/products/1"

3) Utwórz nowy produkt

curl -sS -X POST "http://{HOST}/products" -H "Content-Type: application/json" -d '{"name":"Nowy Produkt","category":"Kategoria","price":123.45,"isActive":true}'

4) (Brak) Usunięcie produktu — DELETE

Brak endpointu DELETE w implementacji. Próba wykonania żądania DELETE na /products/{id} zwróci najprawdopodobniej 404 lub 405 zależnie od konfiguracji hosta i middleware, ponieważ w kontrolerze nie zdefiniowano metody obsługującej usunięcie.

Uwagi implementacyjne i ograniczenia

- Dane są przechowywane w pamięci w postaci statycznej listy w kontrolerze ProductCatalogApi.V1.Controllers.ProductsController. Po restarcie aplikacji lista wraca do wartości początkowych.
- Brak walidacji pól w modelu (np. cena >= 0) — aktualnie przyjmowane są dane przesyłane w body bez dodatkowej walidacji.
- Brak mechanizmu paginacji, filtrowania lub sortowania.

Kod źródłowy (lokacje)

- Kod kontrolera: Controllers/ProductsController.cs
- Model danych: Models/Product.cs
- Dokument wymagań: Documentation/DocumentationRequirements.md

Historia zmian

- 2026-09-03 — Aktualizacja: zaznaczono brak endpointu DELETE w dokumentacji; dopisano sekcję Uwaga — brak endpointu DELETE oraz uzupełniono Historia zmian.
- 2026-09-03 — Wersja początkowa dokumentu (wpis pochodzący z istniejącej dokumentacji).