<script setup lang="ts">
import { ref } from "vue";
import { apiRequest, type CurrentUser, type TokenResponse } from "./api";

const username = ref("");
const password = ref("");
const loading = ref(false);
const error = ref("");
const user = ref<CurrentUser | null>(null);
// Jeton volontairement conservé uniquement en mémoire (pas de localStorage).
const accessToken = ref("");

async function login() {
  loading.value = true;
  error.value = "";
  try {
    const tokens = await apiRequest<TokenResponse>("/auth/login", {
      method: "POST",
      body: JSON.stringify({ userName: username.value, password: password.value }),
    });
    accessToken.value = tokens.accessToken;
    user.value = await apiRequest<CurrentUser>("/auth/me", {}, accessToken.value);
    password.value = "";
  } catch (cause) {
    accessToken.value = "";
    user.value = null;
    error.value = cause instanceof Error ? cause.message : "Erreur de connexion.";
  } finally {
    loading.value = false;
  }
}

function logout() {
  // La déconnexion serveur nécessitera de gérer le refresh token.
  // Ne pas confondre effacement de session locale et révocation serveur.
  accessToken.value = "";
  user.value = null;
}
</script>

<template>
  <main>
    <section class="card" aria-labelledby="page-title">
      <p class="status">Vue 3 · TypeScript · API REST</p>
      <h1 id="page-title">EduGest</h1>
      <p>Espace web des élèves et des professeurs.</p>

      <div v-if="user">
        <h2>Bonjour {{ user.userName }}</h2>
        <p>Rôles : {{ user.roles.join(", ") || "Aucun" }}</p>
        <button type="button" @click="logout">Quitter cette session</button>
      </div>

      <form v-else @submit.prevent="login">
        <label for="username">Identifiant</label>
        <input id="username" v-model="username" autocomplete="username" required />
        <label for="password">Mot de passe</label>
        <input id="password" v-model="password" type="password" autocomplete="current-password" required />
        <p v-if="error" role="alert" class="error">{{ error }}</p>
        <button type="submit" :disabled="loading">{{ loading ? "Connexion…" : "Se connecter" }}</button>
      </form>
      <p class="note">Les pages métier seront ajoutées progressivement selon les droits renvoyés par l’API.</p>
    </section>
  </main>
</template>
