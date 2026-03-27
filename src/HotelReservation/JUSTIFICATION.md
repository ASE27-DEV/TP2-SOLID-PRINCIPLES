# Justification des refactorings SOLID

## SRP (Single Responsibility Principle)

**Problème :** `ReservationService` mélangeait l’accès aux données statiques, la journalisation, les règles de disponibilité et de prix, et l’orchestration du flux de création. Le modèle `Reservation` mélangeait les préoccupations de trois **acteurs** : le **réceptionniste** (cycle de vie et `Cancel()`), le **comptable** (tarification et lignes de facture) et la **gouvernante** (planning du linge). `CheckInService.ProcessCheckIn` entremêlait règles métier, cache et notifications.

**Solution :** Un dépôt (`InMemoryReservationRepository` / `IReservationDataAccess`) pour la persistance, un **domain service** `ReservationDomainService` pour disponibilité et prix de séjour, `BillingCalculator` et `HousekeepingScheduler` pour les règles comptables et ménage, et `ReservationService` comme **service applicatif** qui orchestre uniquement. `CheckInService` ne contient plus que des appels de haut niveau (`Ensure…`, `Refresh…`, `Apply…`, `Notify…`).

**Maintenabilité :** Chaque couche peut évoluer selon un seul type de changement (ex. stockage SQL, règles de prix, scénario métier) sans toucher aux autres. Les acteurs restent identifiables : réceptionniste sur `Reservation`, comptable sur `BillingCalculator` / `InvoiceLineData`, gouvernante sur `HousekeepingScheduler`.

## OCP (Open/Closed Principle)

**Problème :** `CancellationService` utilisait un `switch` sur le nom de politique : toute nouvelle politique (ex. « SuperFlexible ») forçait une modification de la classe.

**Solution :** Interface `ICancellationPolicy` avec une implémentation par politique (`FlexiblePolicy`, `ModeratePolicy`, `StrictPolicy`, `NonRefundablePolicy`) et un `CancellationPolicyRegistry` qui enregistre les politiques. `CancellationService` dépend du registre et ne contient plus de branchement par type de politique.

**Maintenabilité :** On étend le comportement en ajoutant une classe et en l’enregistrant, sans rouvrir `CancellationService`.

**Exemples déjà conformes dans le code initial :**

- `ReservationEventDispatcher` + `IReservationEventHandler` : **Observer** — de nouveaux handlers s’ajoutent sans modifier le dispatcher.
- `IPriceCalculator` + `SeasonalSurchargeDecorator` : **Decorator** — nouvelle règle de prix en enveloppant un calculateur existant.
- `ICleaningPolicy` + implémentations (`StandardCleaningPolicy`, `VipCleaningPolicy`) : **Strategy** — politique de nettoyage interchangeable.

## LSP (Liskov Substitution Principle)

**Problème :** `NonRefundableReservation` implémentait `ICancellable` mais levait une exception dans `Cancel()`, ce qui cassait tout code client traitant uniformément `ICancellable`.

**Solution :** `IReservation` (données communes, sans annulation) et `ICancellableReservation` (héritage de `IReservation` + `Cancel` / `CalculateRefund`). Seules les réservations réellement annulables implémentent `ICancellableReservation`. `CachedRoomRepository` délègue `GetAvailableRooms` au dépôt interne pour des données à jour et invalide le cache sur `Save`.

**Maintenabilité :** Le compilateur exclut les appels invalides ; le dépôt avec cache reste substituable à un dépôt sans cache du point de vue du contrat comportemental.

## ISP (Interface Segregation Principle)

**Problème :** Une grande interface de réservation, une facturation couplée à `Reservation` entier, et `INotificationService` à quatre méthodes alors que chaque client n’en utilise qu’une.

**Solution :** Séparation de `IReservationBillingMetrics`, `IReservationOccupancyMetrics`, `IReservationOverlapQuery` et `IReservationDataAccess` selon les besoins ; `InvoiceGenerator` travaille sur `InvoiceLineData` ; interfaces `IEmailNotificationSender`, `ISmsNotificationSender`, `IPushNotificationSender`, `ISlackNotificationSender` avec `NotificationService` qui implémente les quatre.

**Maintenabilité :** Les consommateurs dépendent de surfaces minimales ; les évolutions de `Reservation` hors champs de facturation n’impactent pas la génération de factures.

## DIP (Dependency Inversion Principle)

**Problème :** `BookingService` instanciait `InMemoryReservationStore` et `FileLogger` ; `HousekeepingService` dépendait directement de `EmailSender`.

**Solution :** `ILogger` et `IReservationRepository` (contrat minimal pour la persistance) sont définis dans `HotelReservation.Services` ; `FileLogger` et `InMemoryReservationStore` implémentent ces abstractions. `ICleaningNotifier` est défini dans `Housekeeping.Domain` ; `EmailCleaningNotifier` adapte `EmailSender` dans l’infrastructure ; `HousekeepingService` ne connaît que `ICleaningNotifier`.

**Maintenabilité :** Le métier dépend d’abstractions ; les implémentations techniques peuvent être remplacées ou simulées en tests sans modifier les services.

---

**Compilation :** `dotnet build src/HotelReservation/HotelReservation.csproj`  
**Exécution :** `dotnet run --project src/HotelReservation/HotelReservation.csproj`
