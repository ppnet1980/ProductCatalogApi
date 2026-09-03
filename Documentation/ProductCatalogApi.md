# ProductCatalogApi

Wersja API: v1

Krótki opis

ProductCatalogApi to proste API katalogu produktów (wersja v1). Umożliwia listowanie produktów, pobieranie pojedynczego produktu oraz tworzenie nowego produktu oraz usuwanie produktu. Działa na najprostszej, in-memory liście w kontrolerze; dane nie są zapisywane do bazy.

Base URL

Domyślny URL aplikacji zależy od konfiguracji hosta (np. http://{HOST}). Kontroler udostępnia mapowany na ścieżkę /products.

Autoryzacja

Brak — API nie wymaga uwierzytelnienia. (Żadna z akcji nie sprawdza autoryzacji.)

Braki / Breaking changes

API nie wymaga uwierzytelnienia i implementacja kontrolera w repozytorium jest prosta (ProductsController). W przyszłości, przy większych zmianach, mogą pojawić się przerwy w kompatybilności.

Endpoints (funkcje)

ProductCatalogApi v1 pozwala na:

- listowanie produktów (GET)
- pobieranie pojedynczego produktu (GET /{id})
- tworzenie nowego produktu (POST)
- usunięcie nowego produktu (DELETE)

Modele

Model: Product
- id (int) — identyfikator produktu
- name (string) — nazwa produktu
- category (string) — kategoria produktu
- price (decimal) — cena produktu
- isActive (bool) — flaga aktywna produktu

Przykłady JSON (lista / pojedynczy element):

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

Endpoi nty

1) Pobierz listę produktów
- Metoda: GET
- Ścieżka: /products
- Parametry: brak
- Odpowiedź: 200 OK — tabela zawiera obiekty Product

2) Pobierz produkt po id
- Metoda: GET
- Ścieżka: /products/{id}
- Parametry: id (int) — identyfikator produktu
- Odpowiedź:
  - 200 OK — zwraca produkt (JSON)
  - 404 Not Found — gdy produkt o podanym id nie istnieje

3) Utwórz nowy produkt
- Metoda: POST
- Ścieżka: /products
- Body: JSON (Product) — obiekt produktu bez pola id (id jest nadawane automatycznie)
- Odpowiedź:
  - 201 Created — lokazlizacja nowo utworzonego zasobu: /products/{id}, odpowiedź zawiera utworzony produkt

4) (Brak) Usuń wpis usystem
- Metoda: DELETE
- Ścieżka: /products
- Body: brak
- Odpowiedź:
  - 200 OK — tabela została zresetowana (w implementacji przykładowej DeleteFromNoContent())

Uwagi: implementacja kontrolera (ProductsController) nie ma zaimplementowanego endpointu HTTP DELETE dla pojedynczego produktu (DELETE /products/{id}) — istnieje metoda Delete(), która zwraca NoContent(), ale nie przyjmuje parametru id. API nie obsługuje usuwania konkretnego produktu przez id.

Przykłady CURL

1) Pobierz listę produktów
curl -s -X GET "http://{HOST}/products"

2) Pobierz produkt po id=1
curl -s -X GET "http://{HOST}/products/1"

3) Utwórz nowy produkt
curl -s -X POST "http://{HOST}/products" -H "Content-Type: application/json" -d '{"name":"Nowy Produkt","category":"Kategoria","price":123.45,"isActive":true}'

4) (Brak) Usuń (wszystkie) — przykładowo
curl -s -X DELETE "http://{HOST}/products"

Uwagi implementacyjne i ograniczenia

- Dane są przechowywane w pamięci w kontrolerze (lista Products w ProductsController). Po restarcie aplikacji dane są tracone.
- Brak walidacji danych w modelu (np. cena (decimal) > 0) i standardowych mechanizmów walidacji po stronie kontrolera.
- Brak mechanizmów paginacji, filtrowania, sortowania.

Kod i pliki

- Kod kontrolera: Controllers/ProductsController.cs
- Model danych: Models/Product.cs
- Dokument wymagania: Documentation/DocumentationRequirements.md

Historia zmian

- 2026-09-03: Aktualizacja dokumentu: dodanie informacji o implementacji kontrolera i modelu, poprawki formatowania.
- 2026-09-03: Aktualizacja: doprecyzowanie, że implementacja Delete() nie usuwa produktu po id i że dane są w pamięci.
- 2026-09-03: Zdarzenie z GitHub: Pull Request #7 (gałąź: ppnet1980-patch-6) został zamknięty. Zaktualizowano dokumentację (2026-09-03) w wyniku zdarzenia PR — potwierdzono obecny stan kodu w repozytorium: ProductsController (GET all, GET by id, POST create, DELETE (bez id) zwracające NoContent) oraz model Product (id, name, category, price, isActive). Brak dodatkowych endpointów ani zmian w kodzie głównej gałęzi.
