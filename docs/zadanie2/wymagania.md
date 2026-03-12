#Ograniczenia:

**L-01 Ograniczenie technologiczne**
System musi być tworzony w silniku Unity 6 z użyciem języka C#, zgodnie z założeniami projektu. 

**L-02 Ograniczenie zasobów medycznych**
Symptomy i choroby dostępne w grze będą oparte na ograniczonym zestawie chorób i symptomów zapisanych w bazie danych projektu. 

**L-03 Ograniczenie czasu realizacji projektu**
System musi zostać zaprojektowany i zaimplementowany w czasie trwania semestru akademickiego. 

**L-04 Ograniczenie językowe**
Interfejs systemu będzie dostępny wyłącznie w języku polskim. 

**L-05 Ograniczenie trybu gry**
System nie będzie posiadał trybu multiplayer ani funkcji współpracy między graczami. 

**L-06 Ograniczenie danych wejściowych**
Gracz może wybierać działania tylko z przygotowanej listy opcji (np. badań i pytań), bez możliwości wpisywania własnych. 

**L-07 Ograniczenie grafiki**
Projekt wykorzystuje proste elementy graficzne 2D zamiast 3D, aby uprościć proces implementacji. 

#Wymagania systemowe: 

**S-01 Liczba użytkowników**
System przewidziany jest dla jednego użytkownika jednocześnie (tryb single player). 

**S-02 Przechowywanie danych**
System musi przechowywać dane przypadków medycznych w plikach JSON. 

**S-03 Oprogramowanie**
System musi działać z wykorzystaniem: 
- Unity 6 
- języka C# 
- systemu kontroli wersji Git + GitHub 

**S-04 Operacje systemowe**
System powinien umożliwiać: 
- wczytanie przypadków medycznych przy uruchomieniu gry 
- zapis postępu gracza 
- odczyt zapisanego postępu 

**S-05 Platforma sprzętowa**
System powinien działać na komputerze osobistym z systemem operacyjnym Microsoft Windows w wersji 10 lub nowszej. 
Minimalne wymagania sprzętowe: 
- rocesor: dwurdzeniowy 2 GHz lub lepszy 
- pamięć RAM: minimum 4 GB 
- karta graficzna obsługująca DirectX 11 
- miejsce na dysku: minimum 1 GB 
- Wymagania funkcjonalne: 

#Wymagania funkcjonalne:

**F-01 Przyjęcie pacjenta Priorytet: M **
Historyjka: Jako gracz (lekarz) chcę otrzymać pacjenta z opisem objawów, aby móc rozpocząć proces diagnozy. 
Test: Po rozpoczęciu rozgrywki system generuje przypadek pacjenta. 

**F-02 Przeprowadzenie wywiadu Priorytet: M **
Historyjka użytkownika: Jako gracz chcę zadawać pytania pacjentowi, aby uzyskać dodatkowe informacje o objawach. 
Test: Po wybraniu pytania system pokazuje odpowiedź pacjenta. 

**F-03 Wykonywanie badań Priorytet: M **
Historyjka użytkownika: Jako gracz chcę zlecać badania medyczne, aby zawęzić listę możliwych chorób. 
Test: Po wybraniu badania system zwraca wynik badania. 

**F-04 Postawienie diagnozy Priorytet: M **
Historyjka użytkownika: Jako gracz chcę wybrać diagnozę z listy chorób, aby zakończyć proces leczenia pacjenta. 
Test: Po wybraniu diagnozy system sprawdza poprawność i wyświetla wynik. 

**F-05 – System czasu klinicznego Priorytet: M **
Historyjka użytkownika: Jako gracz chcę mieć ograniczony czas na diagnozę, aby rozgrywka była bardziej realistyczna. 
Test: Każda akcja zmniejsza dostępny czas. 

**F-06 System punktów Priorytet: S **
Historyjka użytkownika: Jako gracz chcę otrzymywać punkty za poprawne diagnozy, aby śledzić swoje postępy. 
Test: Po poprawnej diagnozie liczba punktów wzrasta. 

**F-07 – Dziennik medyczny Priorytet: S **
Historyjka użytkownika: Jako gracz chcę zapisywać informacje o objawach w dzienniku, aby łatwiej analizować przypadek. 
Test: Gracz może zapisać i odczytać notatki w trakcie gry. 

**F-08 – System nowych przypadków Priorytet: C **
Historyjka użytkownika: Jako gracz chcę otrzymywać kolejne przypadki pacjentów, aby móc kontynuować rozgrywkę. 
Test: Po zakończeniu jednego przypadku system generuje następny.  

#Wymagania niefunkcjonalne:  

**N-01 Wydajność  **
System powinien wczytywać przypadek pacjenta w czasie krótszym niż 2 sekundy. 
Test: Zmierzony czas wczytywania danych. 

**N-02 Niezawodność **
System nie powinien powodować awarii podczas rozgrywki przez leczenia co najmniej 3 przypadków. 
Test: Test długotrwałej rozgrywki. 

**N-03 Użyteczność **
Interfejs użytkownika musi umożliwiać wykonanie każdej akcji w maksymalnie 3 kliknięciach. 
Test: Test użyteczności interfejsu. 

**N-04 Przenośność  **
System musi działać na komputerach z Windows 10 i Windows 11. 
Test: Uruchomienie aplikacji na obu systemach. 

**N-05 Bezpieczeństwo danych **
System nie powinien zapisywać żadnych danych osobowych użytkowników. 
Test: Analiza zapisanych danych i logów aplikacji brak danych osobowych użytkowników.

**N-06 Stabilność **
System nie powinien powodować utraty zapisanego postępu. 
Test: Zamknięcie aplikacji w trakcie działania i ponowne uruchomienie(zapisany postęp zostaje zachowany).

**N-07 Czytelność interfejsu **
UI powinno być czytelne na monitorze 1920×1080. 
Test: Uruchomienie aplikacji w rozdzielczości 1920×1080 i weryfikacja czy wszystkie elementy są widoczne.
 
