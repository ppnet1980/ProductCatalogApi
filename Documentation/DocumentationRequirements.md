# ProductCatalogApi v1 - wymagania dla dokumentacji Markdown

## Cel dokumentu

Ten plik definiuje wymagania dla dokumentacji technicznej API w wersji `v1`, tak aby model AI mógł ją poprawnie wygenerować lub uzupełnić.

## Kontekst wersji v1

Wersja `v1` jest wersją bazową API katalogu produktów.

Obsługiwane endpointy:

- `GET /products`
- `GET /products/{id}`
- `POST /products`

## Wejścia wymagane dla AI

Model powinien otrzymać:

- ścieżkę do istniejącego pliku dokumentacji w folderze `Documentation`, jeśli istnieje
- ścieżkę do tego pliku z wymaganiami
- listę zmienionych plików źródłowych
- pełną treść plików kontrolera i modeli albo diff zmian
- nazwę API i wersję

## Wymagane sekcje w dokumentacji Markdown

Dokumentacja powinna zawierać co najmniej:

1. Tytuł dokumentu
2. Wersję API
3. Historię zmian
4. Krótki opis celu API
6. Listę endpointów
7. Opis każdego endpointu
8. Parametry wejściowe
9. Przykładowe requesty
10. Przykładowe response'y
11. Kody odpowiedzi HTTP
12. Założenia i ograniczenia
13. Informację o danych przykładowych


## Wymagania dla opisu endpointów

Dla każdego endpointu należy opisać:

- metodę HTTP
- ścieżkę
- cel biznesowy endpointu
- parametry ścieżki lub query
- body requestu, jeśli występuje
- strukturę odpowiedzi
- możliwe statusy HTTP
- przypadki błędne

## Wymagania jakościowe dla AI

Model AI:

- ma opierać się wyłącznie na dostarczonych plikach i diffie
- nie może wymyślać endpointów ani parametrów
- ma zachować spójny format Markdown
- ma uwzględnić, że to jest wersja bazowa bez porównania do wcześniejszej wersji

## Wymagania dla historii zmian

Sekcja historii zmian powinna zawierać:

- datę wygenerowania lub aktualizacji
- autora lub nazwę procesu
- krótki opis zakresu zmian

## Sugerowany układ Markdown

```md
# ProductCatalogApi - dokumentacja API

## Historia zmian
## Opis API
## Zakres wersji v1
## Endpointy
## Kody odpowiedzi HTTP
## Ograniczenia i założenia
```
