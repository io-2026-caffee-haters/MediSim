# Przypadki testowe - Symulator Medyczny

## Wymaganie: F-01 Przyjęcie pacjenta

**TC-01.01: Poprawne wygenerowanie nowego przypadku (Happy Path)**

* **Powiązanie z wymaganiem:** F-01
* **Opis:** Weryfikacja, czy po rozpoczęciu nowej gry system poprawnie ładuje i wyświetla dane pierwszego pacjenta.
* **Kroki do wykonania:**
1. Uruchom aplikację.
2. W menu głównym kliknij przycisk "Nowa gra".
3. Obserwuj ekran główny diagnozy.


* **Oczekiwany rezultat:** Na ekranie pojawia się sylwetka pacjenta oraz pole tekstowe zawierające początkowy opis objawów.

**TC-01.02: Wczytywanie i modyfikacja parametrów z pliku JSON**

* **Powiązanie z wymaganiem:** F-01 (oraz S-02)
* **Opis:** Sprawdzenie, czy gra poprawnie zaciąga dane liczbowe z zewnętrznych plików JSON.
* **Kroki do wykonania:**
1. Otwórz folder z plikami JSON zawierającymi bazę gry.
2. Edytuj plik, zmieniając wartości `current score` i `remaining time` na 2137.
3. Uruchom grę i wczytaj stan/przypadek z edytowanego pliku.


* **Oczekiwany rezultat:** Gra wczytuje zmienione dane i wyświetla w UI wynik 2137 oraz czas 2137.

**TC-01.03: Brak dostępnych plików z przypadkami (Edge Case)**

* **Powiązanie z wymaganiem:** F-01
* **Opis:** Weryfikacja zachowania systemu w przypadku braku plików z danymi medycznymi.
* **Kroki do wykonania:**
1. Usuń folder zawierający pliki JSON.
2. Uruchom grę i kliknij "Nowa gra".


* **Oczekiwany rezultat:** Aplikacja nie ulega awarii. Wyświetla się komunikat błędu obsługujący brak danych.

---

## Wymaganie: F-02 Przeprowadzenie wywiadu

**TC-02.01: Akcja wywiadu / Podstawowe badanie**

* **Powiązanie z wymaganiem:** F-02
* **Opis:** Weryfikacja interakcji gracza z dostępnymi opcjami wywiadu (np. wybór RTG klatki piersiowej).
* **Kroki do wykonania:**
1. Otwórz panel opcji.
2. Wybierz konkretną akcję z listy (np. RTG klatki piersiowej).


* **Oczekiwany rezultat:** System przetwarza akcję i wyświetla wynik wybranej opcji.

**TC-02.02: Mechanika Cooldownu**

* **Powiązanie z wymaganiem:** F-02
* **Opis:** Sprawdzenie, czy system poprawnie blokuje spamowanie tej samej akcji.
* **Kroki do wykonania:**
1. Wybierz akcję / badanie i otrzymaj wynik.
2. Natychmiast spróbuj ponownie kliknąć tę samą akcję.


* **Oczekiwany rezultat:** System blokuje ponowne wykonanie akcji (cooldown). Gracz musi odczekać określony czas przed ponownym użyciem.

**TC-02.03: Nadpisywanie wyników na głównym ekranie**

* **Powiązanie z wymaganiem:** F-02
* **Opis:** Weryfikacja braku historii bezpośrednio w oknie wyników (wymuszenie korzystania z dziennika).
* **Kroki do wykonania:**
1. Wykonaj akcję A i odczytaj odpowiedź.
2. Odczekaj cooldown i wykonaj akcję B.


* **Oczekiwany rezultat:** Nowa odpowiedź (z akcji B) całkowicie nadpisuje starą odpowiedź (z akcji A). Stary tekst znika z widoku.

---

## Wymaganie: F-03 Wykonywanie badań

**TC-03.01: Poprawne zlecenie badania laboratoryjnego**

* **Powiązanie z wymaganiem:** F-03
* **Opis:** Sprawdzenie mechaniki dobierania i wykonywania badań z listy.
* **Kroki do wykonania:**
1. Otwórz panel "Badania".
2. Z listy dostępnych opcji wybierz np. "Morfologia krwi".


* **Oczekiwany rezultat:** Badanie zostaje wykonane i wyświetla się odpowiedni komunikat medyczny z wynikiem.

**TC-03.02: Blokada ponownego wykonania badania**

* **Powiązanie z wymaganiem:** F-03
* **Opis:** Weryfikacja działania przycisku po wykorzystaniu badania.
* **Kroki do wykonania:**
1. Zleć badanie i odbierz wynik.
2. Ponownie kliknij przycisk tego samego badania.


* **Oczekiwany rezultat:** Przycisk nadal jest widoczny w UI, ale kliknięcie go nic nie robi (brak reakcji systemu).

**TC-03.03: Badanie niezwiązane z chorobą**

* **Powiązanie z wymaganiem:** F-03
* **Opis:** Sprawdzenie informacji zwrotnej w przypadku błędnego tropu gracza.
* **Kroki do wykonania:**
1. Wczytaj przypadek pacjenta.
2. Zleć badanie, które ewidentnie nie pasuje do obecnego schorzenia.


* **Oczekiwany rezultat:** Wyświetla się dokładny komunikat: "Nie wykryto żadnych objawów."

---

## Wymaganie: F-04 Postawienie diagnozy

**TC-04.01: Prawidłowa diagnoza**

* **Powiązanie z wymaganiem:** F-04
* **Opis:** Weryfikacja głównego warunku sukcesu.
* **Kroki do wykonania:**
1. Przeanalizuj objawy pasujące do choroby X.
2. Otwórz panel diagnoz i kliknij przycisk przypisany do choroby X.


* **Oczekiwany rezultat:** Gra automatycznie weryfikuje wybór i wyświetla ekran sukcesu (pacjent wyleczony).

**TC-04.02: Błędna diagnoza**

* **Powiązanie z wymaganiem:** F-04
* **Opis:** Weryfikacja warunku porażki.
* **Kroki do wykonania:**
1. Przy paczencie chorym na X, kliknij przycisk choroby Y.


* **Oczekiwany rezultat:** System od razu reaguje na kliknięcie, ocenia diagnozę jako błędną i nakłada odpowiednią karę.

**TC-04.03: Weryfikacja natychmiastowego wyboru**

* **Powiązanie z wymaganiem:** F-04
* **Opis:** Potwierdzenie działania mechaniki "one-click diagnosis" bez dodatkowych potwierdzeń.
* **Kroki do wykonania:**
1. Uruchom nową grę.
2. Natychmiast po załadowaniu sceny, bez wykonywania badań, kliknij losową chorobę na panelu diagnozy.


* **Oczekiwany rezultat:** Gra przyjmuje strzał natychmiast po kliknięciu i natychmiast kończy turę pacjenta bez pytania "Czy na pewno?".

---

## Wymaganie: F-05 System czasu klinicznego

**TC-05.01: Odliczanie czasu po wykonanej akcji**

* **Powiązanie z wymaganiem:** F-05
* **Opis:** Sprawdzenie działania mechaniki redukcji czasu.
* **Kroki do wykonania:**
1. Odczytaj aktualny czas w sekundach z interfejsu (np. 80).
2. Wykonaj akcję kosztującą czas.


* **Oczekiwany rezultat:** Wartość czasu spada zgodnie z kosztem, odświeżając się w UI (np. spada do 75).

**TC-05.02: Koniec dostępnego czasu (Game Over)**

* **Powiązanie z wymaganiem:** F-05
* **Opis:** Weryfikacja warunku przegranej z powodu braku czasu.
* **Kroki do wykonania:**
1. Doprowadź licznik sekund blisko zera.
2. Wykonaj akcję, której koszt przekracza pozostały czas, lub po prostu poczekaj na wyzerowanie.


* **Oczekiwany rezultat:** Gra kończy się porażką z komunikatem informującym o upływie czasu.

**TC-05.03: Zróżnicowane koszty czasowe**

* **Powiązanie z wymaganiem:** F-05
* **Opis:** Weryfikacja różnych wag poszczególnych akcji.
* **Kroki do wykonania:**
1. Sprawdź, ile sekund zabiera wywiad.
2. Wczytaj stan od nowa i sprawdź, ile sekund zabiera specjalistyczne badanie.


* **Oczekiwany rezultat:** Zaawansowane badania zabierają inną wartość sekundową niż proste pytania.

---

## Wymaganie: F-06 System punktów

**TC-06.01: Przyrost punktacji**

* **Powiązanie z wymaganiem:** F-06
* **Opis:** Weryfikacja nagrody punktowej.
* **Kroki do wykonania:**
1. Postaw prawidłową diagnozę.


* **Oczekiwany rezultat:** Wynik w UI poprawnie wzrasta o zadeklarowaną wartość.

**TC-06.02: Brak przyrostu przy złej diagnozie**

* **Powiązanie z wymaganiem:** F-06
* **Opis:** Sprawdzenie kar punktowych.
* **Kroki do wykonania:**
1. Odczytaj wynik i postaw błędną diagnozę.


* **Oczekiwany rezultat:** Wynik nie wzrasta (lub jest redukowany).

**TC-06.03: Utrzymanie punktacji między turami**

* **Powiązanie z wymaganiem:** F-06
* **Opis:** Weryfikacja zachowania zmiennych w pamięci w trakcie długiej sesji.
* **Kroki do wykonania:**
1. Zdobądź punkty, przyjmij nowego pacjenta.


* **Oczekiwany rezultat:** Licznik na początku kolejnej tury nie resetuje się do zera.

---

## Wymaganie: F-07 Dziennik medyczny

**TC-07.01: Wprowadzanie tekstu**

* **Powiązanie z wymaganiem:** F-07
* **Opis:** Sprawdzenie edytora notatek.
* **Kroki do wykonania:**
1. Otwórz dziennik, wpisz dowolny tekst z klawiatury i zamknij panel.


* **Oczekiwany rezultat:** Tekst zapisuje się w interfejsie Dziennika.

**TC-07.02: Odczytywanie zapisanych notatek w trakcie tury**

* **Powiązanie z wymaganiem:** F-07
* **Opis:** Weryfikacja utrzymania stanu notatnika przy skakaniu po widokach.
* **Kroki do wykonania:**
1. Zapisz coś w dzienniku, wykonaj badanie, ponownie otwórz dziennik.


* **Oczekiwany rezultat:** Wpisany tekst jest zachowany.

**TC-07.03: Izolacja notatek pacjentów**

* **Powiązanie z wymaganiem:** F-07 (i F-08)
* **Opis:** Sprawdzenie resetowania komponentu przy załadowaniu nowego poziomu.
* **Kroki do wykonania:**
1. Wpisz notatkę do pacjenta A, wylecz go.
2. Załaduj pacjenta B i otwórz dziennik.


* **Oczekiwany rezultat:** Dziennik jest całkowicie pusty.

---

## Wymaganie: F-08 System nowych przypadków

**TC-08.01: Przejście do kolejnej tury**

* **Powiązanie z wymaganiem:** F-08
* **Opis:** Sprawdzenie poprawności resetowania głównej sceny gry.
* **Kroki do wykonania:**
1. Po zakończeniu leczenia kliknij opcję przejścia do następnego przypadku.


* **Oczekiwany rezultat:** System płynnie ładuje kolejnego pacjenta bez wyjścia do menu.

**TC-08.02: Losowość wczytywanych danych**

* **Powiązanie z wymaganiem:** F-08
* **Opis:** Weryfikacja rotacji pacjentów.
* **Kroki do wykonania:**
1. Przejdź przez 3 pacjentów pod rząd.


* **Oczekiwany rezultat:** System losuje różnych pacjentów, a objawy w oknie startowym zmieniają się.

**TC-08.03: Generowanie w nieskończoność**

* **Powiązanie z wymaganiem:** F-08
* **Opis:** Sprawdzenie stabilności gry przy dużej liczbie zrealizowanych przypadków.
* **Kroki do wykonania:**
1. Diagnozuj pacjentów i przechodź do kolejnych poziomów przez dłuższą chwilę (np. 15 razy).


* **Oczekiwany rezultat:** Gra bez końca losuje przypadki z puli; nie następuje błąd wyczerpania bazy ani awaria silnika Unity.
