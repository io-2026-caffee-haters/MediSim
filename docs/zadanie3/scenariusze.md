## U-01: Przyjęcie pacjenta

**Warunki początkowe:**
- Gra jest uruchomiona, gracz znajduje się w menu głównym lub właśnie zakończył poprzedni przypadek.
- System wczytał bazę danych przypadków medycznych z plików JSON.

**Warunki końcowe:**
- Na ekranie widoczny jest nowy pacjent wraz z podstawowymi objawami.
- Uruchomiony został licznik czasu klinicznego.

**Scenariusz główny:**
1. System automatycznie przechodzi do nowego przypadku po zakończeniu poprzedniego.
2. System losuje jeden z przypadków medycznych zapisanych w bazie danych.
3. System wyświetla grafikę pacjenta oraz widoczne na pierwszy rzut oka cechy.
4. System uruchamia odliczanie czasu klinicznego.
5. System dodaje wstępne objawy do dziennika medycznego.


**Odnośniki do wymagań:**
- F-01: Przyjęcie pacjenta
- F-05: System czasu klinicznego
- F-08: System nowych przypadków
- S-02: Przechowywanie danych w JSON

---

## U-02: Przeprowadzenie wywiadu lekarskiego

**Warunki początkowe:**
- Gracz ma aktywnego pacjenta.
- Licznik czasu klinicznego > 0.

**Warunki końcowe:**
- Pacjent opowiada o części symptomów, które posiada lub posiadał.

**Scenariusz główny:**
1. Gracz wybiera opcję „Wywiad” lub klika na ikonę pacjenta.
2. System wyświetla odpowiedź pacjenta w formie tekstowej.

**Odnośniki do wymagań:**
- F-02: Przeprowadzenie wywiadu
- F-05: System czasu klinicznego
- F-07: Dziennik medyczny

---

## U-03: Zlecenie badania dodatkowego

**Warunki początkowe:**
- Gracz ma aktywnego pacjenta.
- Licznik czasu klinicznego > 0.
- Badania mają określone czasy odnowienia i nie są one zablokowane.

**Warunki końcowe:**
- Gracz otrzymuje wynik badania.
- Badanie staje się niedostępne na pewien czas.

**Scenariusz główny:**
1. Gracz wybiera opcję „Badania”.
2. System wyświetla listę dostępnych badań wraz z informacją o ich dostępności.
3. Gracz wybiera konkretne badanie.
4. System sprawdza, czy badanie jest dostępne.
5. System generuje wynik badania zgodny z chorobą pacjenta.
6. System wyświetla wynik.
7. System aktualizuje stan dostępności badania (blokuje).

**Odnośniki do wymagań:**
- F-03: Wykonywanie badań
- F-05: System czasu klinicznego
- F-07: Dziennik medyczny

---

## U-04: Korzystanie z dziennika medycznego

**Warunki początkowe:**
- Gracz ma aktywnego pacjenta.
- Licznik czasu klinicznego > 0.
- Dziennik jest dostępny do użytku.

**Warunki końcowe:**
- Gracz przegląda zebrane informacje.
- Opcjonalnie: gracz może dodać własną notatkę.

**Scenariusz główny:**
1. Gracz klika na ikonę notatnika.
2. System wyświetla ekran dziennika z listą wszystkich chorób i ich objawów.
3. Gracz może przewijać listę i czytać informacje.
4. Gracz może (opcjonalnie) dodać własną notatkę tekstową, wpisując ją w odpowiednim polu i zatwierdzając.
5. Gracz zamyka dziennik i wraca do głównego ekranu gry.

**Odnośniki do wymagań:**
- F-07: Dziennik medyczny

---

## U-05: Postawienie diagnozy

**Warunki początkowe:**
- Gracz ma aktywnego pacjenta.
- Licznik czasu klinicznego > 0.
- Dostępna jest lista możliwych chorób.

**Warunki końcowe:**
- Gracz otrzymuje informację o poprawności diagnozy.
- Punkty gracza zostają zaktualizowane.
- Czas kliniczny zostaje zaktualizowany.
- Przypadek zostaje zakończony.
- System przechodzi do nowego przypadku lub gdy na czasie klinicznym zostało mniej niż 10 sekund czasu to gracz dostaje wybór czy dobrać kolejnego pacjenta.

**Scenariusz główny:**
1. Gracz wybiera opcję „Diagnoza”.
2. System wyświetla listę wszystkich chorób dostępnych w grze.
3. Gracz wybiera jedną chorobę z listy.
4. System porównuje wybraną chorobę z prawdziwą chorobą pacjenta.
5. *Jeśli diagnoza poprawna:*
   - System dodaje punkty.
   - System dodaje sekundy do czasu klinicznego.
   - System wyświetla komunikat sukcesu.
6. *Jeśli diagnoza błędna:*
   - System odejmuje punkty.
   - System odejmuje sekundy od czasu klinicznego.
   - System wyświetla komunikat błędu.
7. System kończy obecny przypadek.
8. System automatycznie dodaje nowego pacjenta lub daje wybór graczowi w przypadku małej ilości czasu.

**Odnośniki do wymagań:**
- F-04: Postawienie diagnozy
- F-05: System czasu klinicznego
- F-06: System punktów
- F-08: System nowych przypadków

---

## U-06: Zapisanie stanu gry (opcjonalny)

**Warunki początkowe:**
- Gra jest w trakcie rozgrywki.
- System ma prawo zapisu na dysku.

**Warunki końcowe:**
- Aktualny stan gry (punkty, czas, aktywny pacjent, dziennik, historia przypadków) zostaje zapisany w pliku JSON.

**Scenariusz główny:**
1. Gracz wybiera opcję „Zapisz grę” z menu.
2. System wyświetla okno dialogowe z prośbą o potwierdzenie.
3. System zapisuje dane do pliku JSON.
4. System wyświetla komunikat „Gra zapisana”.

**Odnośniki do wymagań:**
- S-04: Operacje systemowe (zapis postępu)

---

## U-07: Wczytanie stanu gry (opcjonalny)

**Warunki początkowe:**
- Gra jest uruchomiona, gracz znajduje się w menu głównym.
- Istnieje zapisany plik stanu gry.

**Warunki końcowe:**
- Stan gry zostaje przywrócony (punkty, czas, aktywny pacjent, dziennik).
- Gracz kontynuuje rozgrywkę od momentu zapisu.

**Scenariusz główny:**
1. Gracz wybiera opcję „Wczytaj grę” z menu.
2. System wyświetla listę dostępnych zapisów.
3. Gracz wybiera jeden z zapisów.
4. System wczytuje dane z pliku JSON.
5. System przywraca stan gry i przechodzi do ekranu rozgrywki z wczytanym pacjentem.

**Odnośniki do wymagań:**
- S-04: Operacje systemowe (odczyt zapisanego postępu).
