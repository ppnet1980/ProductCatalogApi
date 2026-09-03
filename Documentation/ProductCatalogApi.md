# ProductCatalogApi

Wersja API: v1

Krótki opis

ProductCatalogApi to proste API katalogu produktów (wersja v1). Umożliwia listowanie produktów, pobieranie pojedynczego produktu oraz tworzenie nowych produktów. Działa najprościej — w pamięci trzymana lista w kontrolerze; dane nie są zapisywane do bazy.

Base URL

Domyślny URL aplikacji zależy od konfiguracji hosta (np. http://{HOST}). Kontroler udostępnia mapowany na ścieżkę /products.

Autoryzacja

Brak — API nie wymaga uwierzytelnienia. (Zgodnie z aktualnym kodem kontrolera.)

Braki / Breaking changes

API nie wymaga uwierzytelnienia i implementacja kontrolera w repozytorium jest prosta (ProductsController trzyma dane w pamięci). W przyszłości, przy dodawaniu trwałego magazynu lub autoryzacji, mogą wystąpić zmiany w zachowaniu i wymaganiach.

Endpointy (funkcje)

ProductCatalogApi v1 pozwala na:
- listowanie produktów (GET)
- pobieranie pojedynczego produktu (GET /{id})
- tworzenie nowego produktu (POST)
- usunięcie wszystkich produktów (DELETE)

Modele

Model: Product
- id (int) — identyfikator produktu
- name (string) — nazwa produktu
- category (string) — kategoria produktu
- price (decimal) — cena produktu
- isActive (bool) — flaga aktywności produktu

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

Endpoints

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
  - 200 OK — zwraca produkt (JSON)
  - 404 Not Found — gdy produkt o podanym id nie istnieje

3) Utwórz nowy produkt
- Metoda: POST
- Ścieżka: /products
- Body: JSON (Product) — obiekt produktu bez pola id (id jest nadawane automatycznie)
- Odpowiedź:
  - 201 Created — lokazlizacja nowo utworzonego zasobu: /products/{id}, odpowiedź zawiera utworzony produkt

4) (Brak) Usuń wszystkie produkty — DELETE
- Metoda: DELETE
- Ścieżka: /products
- Body: brak
- Odpowiedź:
  - 200 OK — tabela została opróżniona (implementacja w kontrolerze zwraca NoContent())

Uwaga: implementacja kontrolera (ProductsController) nie ma zaimplementowanego endpointu HTTP DELETE dla pojedynczego produktu (DELETE /products/{id}) — istnieje jedynie metoda DeleteAll() wywoływana na ścieżce DELETE /products zwracająca NoContent(). API nie obsługuje usuwania pojedynczego rekordu ani aktualizacji (PUT/PATCH) zgodnie z aktualnym kodem.

Przykłady CURL

1) Pobierz listę produktów
curl -s -X GET "http://{HOST}/products"

2) Pobierz produkt po id=1
curl -s -X GET "http://{HOST}/products/1"

3) Utwórz nowy produkt
curl -s -X POST "http://{HOST}/products" -H "Content-Type: application/json" -d '{"name":"Nowy Produkt","category":"Kategoria","price":123.45,"isActive":true}'

4) (Brak) Usuń — DELETE (wszystkie)
curl -s -X DELETE "http://{HOST}/products"

Uwagi implementacyjne i ograniczenia

- Dane są przechowywane w pamięci w kontrolerze (lista Products w ProductsController). Po restarcie aplikacji dane są tracone.
- Brak walidacji danych w modelu (np. cena (decimal) > 0) — aktualnie prosta implementacja przyjmuje obiekt (np. cena >= 0) i dodaje do listy.
- Brak mechanizmów paginacji, filtrowania, sortowania.

Kod i pliki

- Kod kontrolera: Controllers/ProductsController.cs
- Model danych: Models/Product.cs
- Dokument wymagań: Documentation/DocumentationRequirements.md

Historia zmian

- 2026-09-03: Aktyalizacja dokumentacji: dodano informację o zamknięciu Pull Request #6 (gałąź: ppnet1980-patch-5). Brak zmian w API — kontroler nie został zmodyfikowany w sposób wpływający na dokumentowane endpointy.

- 2026-09-03: Aktualizacja: doprecyzowano endpointy GET/POST/DELETE i przykłady użycia; opis implementacji pamięciowej oraz ograniczeń.

