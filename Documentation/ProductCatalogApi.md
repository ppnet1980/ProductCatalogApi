# ProductCatalogApi

Wersja API: v1

Krótski opis

ProductCatalogApi to proste API katalogu produktów (wersja v1). Umożliwia listowanie produktów, pobieranie pojedynczego produktu, tworzenie nowych pozycji oraz aktualizację statusu (flagi isActive). Działania są wykonywane w pamięci (statyczna lista w kontrolerze) — dane nie są zapisywane do bazy.

Base URL

Domyślny URL aplikacji zależy od konfiguracji hosta (np. http://{HOST}). Kontroler jest mapowany na ścieżkę /products.

Autoryzacja

Brak — API nie wymaga uwierzytelnienia w aktualnej implementacji.

Zakres funkcji

ProductCatalogApi v1 pozwala na:
- listowanie produktów (GET /products)
- pobieranie pojedynczego produktu (GET /products/{id})
- tworzenie nowego produktu (POST /products)
- aktualizację statusu isActive produktu (PATCH /products/{id}/status)

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
- Body: JSON (Product) — obiekt produktu (pole id jest ignorowane na wejściu)
- Zachowanie implementacji: kontroler tworzy nowy obiekt Product i nadaje mu Id: jeżeli lista jest pusta Id=1, w przeciwnym razie Id = Max(existing Id) + 1. Następnie dodaje produkt do listy.
- Odpowiedź:
  - 201 Created — nagłówek Location wskazuje na /products/{id}, odpowiedź zawiera utworzony obiekt Product (CreatedAtAction)

4) Aktualizuj status produktu (isActive)
- Metoda: PATCH
- Ścieżka: /products/{id}/status
- Parametry:
  - id (int) — identyfikator produktu w ścieżce
- Body: boolean — zawartość body to pojedyncza wartość true/false (przykładowo: true)
- Odpowiedź:
  - 200 OK — zwraca zaktualizowany obiekt Product (jeżeli produkt istnieje)
  - 404 Not Found — zwraca NotFound z wiadomością (np. "Nie znaleziono produktu o ID = {id}.") gdy produkt nie istnieje

Brak endpointu DELETE

W aktualnej implementacji kontrolera (Controllers/ProductsController.cs) nie ma metody odpowiadającej na żądania HTTP DELETE. Oznacza to, że usuwanie produktów nie jest zaimplementowane.

Przykłady cURL

1) Pobierz listę produktów

curl -sS -X GET "http://{HOST}/products"

2) Pobierz produkt po id=1

curl -sS -X GET "http://{HOST}/products/1"

3) Utwórz nowy produkt

curl -sS -X POST "http://{HOST}/products" -H "Content-Type: application/json" -d '{"name":"Nowy Produkt","category":"Kategoria","price":123.45,"isActive":true}'

4) Zaktualizuj status produktu (ustaw isActive = true)

curl -sS -X PATCH "http://{HOST}/products/3/status" -H "Content-Type: application/json" -d 'true'

Uwagi implementacyjne i ograniczenia

- Dane są przechowywane w pamięci w postaci statycznej listy Products w kontrolerze ProductCatalogApi.V1.Controllers.ProductsController. Po restarcie aplikacji lista wraca do wartości początkowych.
- Brak rozbudowanej walidacji pól (np. sprawdzenia, że price >= 0) — dane z body są wykorzystywane bez dodatkowej walidacji.
- Brak paginacji, filtrowania i sortowania.
- Brak endpointu DELETE — nie ma możliwości usunięcia produktu przez API w obecnej implementacji.

Kod źródłowy (lokacje)

- Kontroler: Controllers/ProductsController.cs
- Model danych: Models/Product.cs
- Wymagania dokumentacji: Documentation/DocumentationRequirements.md

Historia zmian

- 2026-09-03 — Aktualizacja: uaktualniono dokumentację zgodnie z aktualnym kodem źródłowym; dodano informację o endpointzie PATCH /products/{id}/status oraz potwierdzono brak DELETE.
- 2026-09-03 — Aktualizacja: wcześniejsze poprawki i uzupełnienia dokumentacji.

