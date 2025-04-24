(function waitForSwaggerUI() {
    if (!window.ui || !window.ui.authActions) {
        setTimeout(waitForSwaggerUI, 100); // 🔁 Try again after 100ms
        return;
    }

    const originalFetch = window.fetch;

    window.fetch = function () {
        return originalFetch.apply(this, arguments).then(async response => {
            try {
                if (response.url.includes("/login")) {
                    const cloned = response.clone();
                    const json = await cloned.json();

                    // Adjust based on your API response
                    const token = json?.data?.token;

                    if (token) {
                        const bearerToken = `Bearer ${token}`;
                        window.localStorage.setItem('jwtToken', bearerToken);

                        // 👇 Inject token into Swagger UI auth
                        window.ui.authActions.authorize({
                            Bearer: {
                                name: "Bearer",
                                schema: {
                                    type: "apiKey",
                                    in: "header",
                                    name: "Authorization"
                                },
                                value: bearerToken
                            }
                        });

                        console.log("✅ JWT token injected into Swagger!");
                    }
                }
            } catch (err) {
                console.error("❌ JWT Auto Auth Error:", err);
            }

            return response;
        });
    };
})();
