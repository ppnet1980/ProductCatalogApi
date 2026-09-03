# ProductCatalogApi

Wersja API: v1

Krótki opis

ProductCatalogApi to proste API katalogu produktów (wersja v1). Umożliwia listowanie produktów, pobieranie szczegółów produktu, tworzenie oraz usuwanie produktów. Dane przechowywane są w pamięci aplikacji (lista statyczna w kontrolerze).

Base URL

Domyślny URL aplikacji zależy od konfiguracji hosta (np. http://{HOST}). Kontroler jest zmapowany na ścieżkę /products.

Autoryzacja

Brak - API nie wymaga autoryzacji. (Zgodnie z implementacją kontrolera w repozytorium nie ma mechanizmu uwierzytelniania ani autoryzacji.)

Zakres funkcji

ProductCatalogApi v1 pozwala na:
- listowanie produktów
- pobieranie pojedynczego produktu po identyfikatorze
- tworzenie nowego produktu
- usuwanie produktu

Modele

Model: Product
- id (int) — identyfikator produktu
- name (string) — nazwa produktu
- category (string) — kategoria produktu
- price (decimal) — cena produktu
- isActive (bool) — flaga aktywności produktu

Przykład JSON (lista / przykładowe wpisy z implementacji):
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

Endpoints

1) Pobierz listę produktów
- Metoda: GET
- Ścieżka: /products
- Parametry: brak
- Odpowiedź: 200 OK - tablica obiektów Product

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
- Body: JSON — obiekt Product (bez lub z id; serwer ustawi id automatycznie)
- Odpowiedź:
  - 201 Created — lokalizacja nagłówka Location wskazuje /products/{id}, odpowiedź zawiera utworzony obiekt Product

Przykład żądania (POST):
{
  "name": "Nowy Produkt",
  "category": "Kategoria",
  "price": 123.45,
  "isActive": true
}

4) Usuń produkt po id
- Metoda: DELETE
- Ścieżka: /products/{id}
- Parametry: id (int)
- Odpowiedź:
  - 200 OK — zwraca usunięty obiekt Product gdy usunięcie powiodło się
  - 404 Not Found — gdy produkt nie został znaleziony

Przykłady curl

1) Pobierz listę produktów
curl -sS -X GET "http://{HOST}/products"

2) Pobierz produkt po id=1
curl -sS -X GET "http://{HOST}/products/1"

3) Utwórz nowy produkt
curl -sS -X POST "http://{HOST}/products" -H "Content-Type: application/json" -d '{"name":"Nowy Produkt","category":"Kategoria","price":123.45,"isActive":true}'

4) Usuń produkt o id=4
curl -sS -X DELETE "http://{HOST}/products/4"

Uwagi implementacyjne i ograniczenia

- Dane są przechowywane w pamięci (statyczna lista w ProductsController). Nie ma trwałego magazynu danych — restart aplikacji przywraca listę do wartości zdefiniowanych w kodzie.
- Brak autoryzacji/autentykacji i ograniczeń dostępu.
- Id produktu jest generowane po stronie serwera przy tworzeniu (kolejne id = max(id) + 1 lub 1 gdy lista pusta).
- Przykładowe dane początkowe (z kodu): trzy produkty (id 1,2,3) — patrz sekcja "Modele".

Historia zmian

- 2026-09-03 — Uaktualniono dokumentację zgodnie z aktualnym kodem repozytorium:
  - Potwierdzono dostępne endpointy i odpowiednie kody odpowiedzi (GET list, GET by id, POST tworzenie z 201 Created, DELETE zwracające usunięty obiekt lub 404).
  - Dodano sekcję "Uwagi implementacyjne i ograniczenia" opisującą przechowywanie danych w pamięci oraz brak autoryzacji.
  - Zaktualizowano przykłady żądań i przykładowe dane zgodnie z implementacją w kontrolerze.

Źródła

Kod źródłowy kontrolera: Controllers/ProductsController.cs
Model danych: Models/Product.cs
Dokument wymagającego formatu: Documentation/DocumentationRequirements.md

Repozytorium: https://github.com/ppnet1980/ProductCatalogApi
