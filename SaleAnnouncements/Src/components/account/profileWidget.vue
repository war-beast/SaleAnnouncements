﻿<template>
	<ul class="navbar-nav">
		<li class="nav-item" v-if="isLogged">
			<a class="nav-link text-dark" href="/Profile/" title="Manage">Привет <span style="text-decoration: underline;">{{userEmail}}</span></a>
		</li>
		<li class="nav-item" v-else>
			<a class="nav-link text-dark" href="/Identity/Account/Register">Регистрация</a>
		</li>
		<li class="nav-item" v-if="isLogged">
			<button type="button" class="nav-link btn btn-link text-dark" v-on:click="logOut">Выйти</button>
		</li>
		<li class="nav-item" v-else>
			<a class="nav-link text-dark" href="/Identity/Account/Login">Вход</a>
		</li>
	</ul>
</template>

<script lang="ts" src="./profileWidget.ts"></script>