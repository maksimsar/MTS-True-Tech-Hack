// TODO: при выделении слоёв перенести каждый контракт
//       в Api/Contracts/Schema/...

namespace MTS.TrueTechHack.Models.Contracts;

/// <summary>
/// Контракты внешнего HTTP‑API для работы со схемами
/// и чат‑редактированием (запросы ↔ ответы).
/// </summary>
public static class ContractDocs { } // «заглушка»‑пространство имён (Swagger игнорирует)

#region ---------- Requests ----------

/// <summary>
/// Тело запроса на создание новой JSON‑схемы.
/// </summary>
public sealed record CreateSchemaRequest(
    /// <summary>Читаемое название схемы (отображается в UI).</summary>
    string Name,
    /// <summary>Краткое описание бизнес‑процесса / интеграции.</summary>
    string Description
);

/// <summary>
/// Запрос с очередным сообщением пользователя в чат‑редакторе схемы.
/// </summary>
public sealed record ChatRequest(
    /// <summary>Текст реплики пользователя.</summary>
    string Text
);

#endregion

#region ---------- Responses ----------

/// <summary>
/// Ответ API после генерации схемы GPT‑ассистентом.
/// </summary>
public sealed record SchemaResponse(
    /// <summary>Идентификатор новой схемы.</summary>
    int Id,
    /// <summary>Имя схемы (как в запросе).</summary>
    string Name,
    /// <summary>Сгенерированная JSON‑схема (строка).</summary>
    string JsonSchema
);

/// <summary>
/// Один элемент истории диалога (для фронта).
/// </summary>
public sealed record ChatMessageResponse(
    /// <summary>Текст сообщения (пользователя или ассистента).</summary>
    string Text,
    /// <summary>True — сообщение от пользователя; False — от GPT.</summary>
    bool IsFromUser,
    /// <summary>Момент отправки сообщения (UTC).</summary>
    DateTime Timestamp
);

#endregion