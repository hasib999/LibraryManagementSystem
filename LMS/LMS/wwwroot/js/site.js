// Shared client-side behavior is intentionally minimal for this MVC project.
document.querySelectorAll("[data-confirm]").forEach((button) =>
    button.addEventListener("click", (event) => {
        if (!confirm(button.dataset.confirm)) event.preventDefault();
    }),
);
