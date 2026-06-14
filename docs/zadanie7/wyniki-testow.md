# Wyniki testów funkcjonalnych

| ID Testu | Rzeczywisty rezultat | Wynik (OK/NOK) | Zrzut ekranu (dowód) |
| :--- | :--- | :--- | :--- |
| **TC-01.01** | Pacjent ładuje się poprawnie, początkowe objawy są widoczne w interfejsie. | OK | `screenshoty/tc-01-01.jpg` |
| **TC-01.02** | Gra wczytała zedytowany plik JSON i ustawiła wartości `current score` oraz `remaining time` na 2137. | OK | `screenshoty/tc-01-02.jpg` |
| **TC-01.03** | Silnik nie ulega awarii, system prawidłowo wyświetla komunikat o braku plików medycznych. | OK | `screenshoty/tc-01-03.jpg` |
| **TC-02.01** | Po wybraniu opcji (np. RTG klatki piersiowej) gra zwraca poprawny wynik badania w panelu. | OK | `screenshoty/tc-02-01-przed.jpg`<br>`screenshoty/tc-02-01-po.jpg` |
| **TC-02.02** | Mechanika cooldownu działa poprawnie – akcja jest zablokowana przez określony czas po pierwszym użyciu. (trudno pokazać na ss, bo czas w grze mija) | OK | `screenshoty/tc-02-02-przed.jpg`<br>`screenshoty/tc-02-02-po.jpg` |
| **TC-02.03** | Tekst z wynikami nowego badania natychmiast i całkowicie nadpisuje stary wynik na ekranie głównym. | OK | `screenshoty/tc-02-03-przed.jpg`<br>`screenshoty/tc-02-03-po.jpg` |
| **TC-03.01** | Po zleceniu wybranego badania (np. Morfologia krwi) odpowiedni komunikat z wynikiem pojawia się w UI. | OK | `screenshoty/tc-03-01.jpg` |
| **TC-03.02** | Przycisk wykonanego wcześniej badania nadal jest widoczny, ale po ponownym kliknięciu gra nie wykonuje żadnej akcji. | OK | `screenshoty/tc-03-02.jpg` |
| **TC-03.03** | Po zleceniu badania niezwiązanego z objawami system wyświetla przewidziany komunikat: "Nie wykryto żadnych objawów." | OK | `screenshoty/tc-03-03.jpg` |
| **TC-04.01** | Pojedyncze kliknięcie w poprawną chorobę automatycznie weryfikuje wybór i kończy leczenie sukcesem. | OK | `screenshoty/tc-04-01-przed.jpg`<br>`screenshoty/tc-04-01-po.jpg` |
| **TC-04.02** | Kliknięcie w błędną chorobę automatycznie nakłada karę punktową / kończy się komunikatem o błędzie. | OK | `screenshoty/tc-04-02.jpg` |
| **TC-04.03** | System "one-click diagnosis" przetwarza wybór natychmiast po kliknięciu choroby w panelu, bez żądania dodatkowego potwierdzenia. | OK | (trudno pokazać, pokazano na poprzednich ss np. 04-01) |
| **TC-05.01** | Po wykonaniu akcji zegar poprawnie zredukował wartość, np. z 80 sekund zmniejszył się do 75 sekund. | OK | (trudno pokazać odjęty czas na ss ze względu na upływający czas w grze. Działa) |
| **TC-05.02** | Po upływie wyznaczonego czasu w sekundach gra poprawnie wymusza ekran końca gry (Game Over). | OK | `screenshoty/tc-05-02.jpg` |
| **TC-05.03** | Poszczególne działania odejmują zróżnicowaną liczbę sekund z głównego zegara gry. | OK | (trudno pokazać odjęty czas na ss ze względu na upływający czas w grze. Działa) |
| **TC-06.01** | Po postawieniu poprawnej diagnozy liczba zdobytych punktów na ekranie wzrasta. | OK | `screenshoty/tc-06-01-przed.jpg`<br>`screenshoty/tc-06-01-po.jpg` |
| **TC-06.02** | Błędna diagnoza nie powoduje przyrostu puli punktów u gracza. | OK | `screenshoty/tc-06-02-przed.jpg`<br>`screenshoty/tc-06-02-po.jpg` |
| **TC-06.03** | Zgromadzone punkty utrzymują się w pamięci i nie zerują po wczytaniu kolejnego pacjenta. | OK | `screenshoty/tc-06-03-koniec-tury.jpg`<br>`screenshoty/tc-06-03-start-nowej.jpg` |
| **TC-07.01** | Wprowadzany tekst prawidłowo i na bieżąco wyświetla się w polu tekstowym dziennika. | OK | `screenshoty/tc-07-01.jpg` |
| **TC-07.02** | Po zamknięciu i ponownym otwarciu dziennika podczas trwania tury, wpisane notatki pozostają nienaruszone. | OK | `screenshoty/tc-07-02-przed.jpg`<br>`screenshoty/tc-07-02-po.jpg` |
| **TC-07.03** | Po załadowaniu kolejnego przypadku dziennik wciąż zawiera notatki pacjenta z poprzedniej tury (brak czyszczenia bufora). | **NOK** | `screenshoty/tc-07-03-koniec-tury.jpg`<br>`screenshoty/tc-07-03-nowy-pacjent.jpg` |
| **TC-08.01** | System płynnie przechodzi do załadowania nowej tury bezpośrednio po kliknięciu odpowiedniego przycisku. | OK | `screenshoty/tc-08-01-przed.jpg`<br>`screenshoty/tc-08-01-po.jpg` |
| **TC-08.02** | Mechanika dobiera pacjentów losowo, przez co początkowe objawy i ścieżka diagnozy ulegają zmianie. | OK | (ss te same co w TC-08.01, nowy pacjent powstał) |
| **TC-08.03** | Gra stabilnie losuje kolejne przypadki w nieskończoność bez napotykania na błąd wyczerpania bazy danych. | OK | `screenshoty/tc-08-03.jpg` |
