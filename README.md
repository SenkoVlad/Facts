# План реализация проекта "Только факты"

**Внимание!** *Данный план является предварительным, потому что список задач, которые предстоит решить в процессе создания новой версии приложения может подвергнуться существенной переработки. По мере создания функциональности план будет пересмотрен.*

* [ ] Регистрация репозитория github.com
* [] Создание проекта и его подготовка к разработке
    * [ ] Настройка логирования в log-файл
    * [ ] Реализация "вход/выход" на сайте
* [ ] Настройка добавления учетной записи при создании новой базы (SEED)
* [ ] Создание сущностей (классов) и конфигрурирование сущностей через fluent API (EntityTypeConfiguration)
    * [ ] Fact
    * [ ] Tag
    * [ ] Notification
* [ ] Создание EF-миграции и базы данных
* [ ] Настройка возможности переноса данных из старой БД в новую БД
* [ ] Создание ViewModels для сущностей и настройка маппинга (Automapper)
* [ ] Изменение шаблонов от Microsoft.AspNetCore.Identity UI
* [ ] Шаблоны ASP.NET MVC (_Layout) и управление ими
* [ ] Реализация в ApplicationDbContext автоматическое обновление свойств CreatedAt, UpdatedAt, CreatedBy, UpdatedBy (унаследованных от типа Auditable)
* [ ] Определить маршруты для MVC
* [ ] Mediatr: Инфраструктура для Notification
  * [ ] Mediatr: NotificationBase
  * [ ] Mediatr: NotificationHandlerBase
  * [ ] Mediatr: ErrorNotification
  * [ ] Mediatr: ErrorNotificationHandler
  * [ ] Mediatr: FeedbackNotification
  * [ ] Mediatr: FeedbackNotificationHandler
* [ ] Объединение и минификация статических ресурсов в ASP.NET Core 
* [ ] Создание главной страницы (без разбиение на страницы)
  * [ ] Метод в контроллере FactsController
  * [ ] Mediatr: FactGetPagedRequest
  * [ ] Mediatr: FactGetPagedResponse
* [ ] TagHelper: Создание pager: IPagedListTagHelperService, PagerData, PagedListHelper
* [ ] Подключение Pager на главную страницу
* [ ] Страница детального просмотра выбранного факта
  * [ ] Настройка и проверка Route для Show.cshtml 
  * [ ] Разметка страницы отображения выбранного факта
  * [ ] Mediatr: FactGetByIdRequest
  * [ ] Mediatr: FactGetByIdResponse
* [ ]Реалиазиция фильтрации фактов на главной странице
    * [ ] По метке (tag)
    * [ ] По слову поиска (search)
* [ ] Страница "Обратная связь" (Backend) 
  * [ ] Добавление записей в список уведомлений (Notification)
  * [ ] Mediatr: FeedbackNotificationRequest
  * [ ] Mediatr: FeedbackNotificationResponse
  * [ ] Метод генерирующий картинку (reCapture) 
  * [ ] Добавление проверочной картинки (reCapture) на страницу FeedBack
* [ ] Blazor: Подключаем Toastr через component Blazor
* [ ] Blazor: Копируем ссылку через component Blazor
* [ ] Администратор: Страница "панель управления" (навигатор управления)
* [ ] Администратор: Страница "добавление факта"
   * [ ] Blazor: Используем component Blazor для поиска по ключу уже существующих фактов
   * [ ] Blazor: Используем component Blazor для поиска тегов для нового факта
* [ ] Администратор: Страница "редактирования факта"
   * [ ] Blazor: Используем component Blazor для поиска по ключу уже существующих фактов
   * [ ] Blazor: Используем component Blazor для поиска тегов для обновления факта
* [ ] Администратор: Страница "удаления факта"
* [ ] Администратор: Реализация постраничного просмотра списка сообщений (Notification)
* [ ] Администратор: Страница "отправки почтового сообщения"
* [ ] HostedService: Сработка по расписанию (Cron)
  * [ ] Отправка почты. Создание IEmailService
  * [ ] INotificationProvider обработчик Notification, отправка сообщений и обновление статуса отправки
  * [ ] Реализация BackgroundWorker для отправки почтовых писем из таблицы Notification

# Дополнительно
* [ASP.NET Core MVC "Только факты" (NET5.0)](https://github.com/Calabonga/Facts/wiki)
* [О приложении](https://github.com/Calabonga/Facts/wiki/%D0%9E-%D0%BF%D1%80%D0%B8%D0%BB%D0%BE%D0%B6%D0%B5%D0%BD%D0%B8%D0%B8)
* [Цели и задачи проекта](https://github.com/Calabonga/Facts/wiki/%D0%A6%D0%B5%D0%BB%D0%B8-%D0%B8-%D0%B7%D0%B0%D0%B4%D0%B0%D1%87%D0%B8-%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%B0)
* [Затронутые аспекты](https://github.com/Calabonga/Facts/wiki/%D0%97%D0%B0%D1%82%D1%80%D0%BE%D0%BD%D1%83%D1%82%D1%8B%D0%B5-%D0%B0%D1%81%D0%BF%D0%B5%D0%BA%D1%82%D1%8B)
* [Основные функциональные возможности](https://github.com/Calabonga/Facts/wiki/%D0%9E%D1%81%D0%BD%D0%BE%D0%B2%D0%BD%D1%8B%D0%B5-%D1%84%D1%83%D0%BD%D0%BA%D1%86%D0%B8%D0%BE%D0%BD%D0%B0%D0%BB%D1%8C%D0%BD%D1%8B%D0%B5-%D0%B2%D0%BE%D0%B7%D0%BC%D0%BE%D0%B6%D0%BD%D0%BE%D1%81%D1%82%D0%B8)
* [Пользователи сайта могут](https://github.com/Calabonga/Facts/wiki/%D0%92%D0%BE%D0%B7%D0%BC%D0%BE%D0%B6%D0%BD%D0%BE%D1%81%D1%82%D0%B8-%D0%B4%D0%BB%D1%8F-%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D1%82%D0%B5%D0%BB%D1%8F)
* [Администратор сайта может](https://github.com/Calabonga/Facts/wiki/%D0%92%D0%BE%D0%B7%D0%BC%D0%BE%D0%B6%D0%BD%D0%BE%D1%81%D1%82%D0%B8-%D0%B4%D0%BB%D1%8F-%D0%B0%D0%B4%D0%BC%D0%B8%D0%BD%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%82%D0%BE%D1%80%D0%B0)
* [Видео материалы](https://github.com/Calabonga/Facts/wiki/%D0%92%D0%B8%D0%B4%D0%B5%D0%BE-%D0%BC%D0%B0%D1%82%D0%B5%D1%80%D0%B8%D0%B0%D0%BB%D1%8B)
