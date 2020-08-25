<template>
	<div class="row">
		<form v-bind:class="{ 'was-validated': !formValid }">
			<h4 class="h4">Добавить объявление <span class="text-muted">(все поля заполнить обязательно)</span></h4>
			<div class="text-danger" v-if="createError != ''">{{createError}}</div>
			<div class="form-group">
				<label for="emailField">Название (заголовок объявления)</label>
				<input id="emailField" class="form-control" required="" type="email" v-model="offer.subject" />
				<div class="invalid-feedback">
					Пожалуйста введите Название
				</div>
			</div>

			<div class="form-group">
				<label for="passwordField">Описание</label>
				<textarea id="passwordField" class="form-control" required="" v-model="offer.description"></textarea>
				<div class="invalid-feedback">
					Пожалуйста введите описание
				</div>
			</div>

			<div class="custom-file">
				<input type="file" id="files" v-on:change="handleFilesUpload" />
				<label class="custom-file-label" for="files">Выберите фото</label>
			</div>

			<div class="row">
				<div class="col-sm-6">
					<div class="form-group">
						<label for="passwordField">Цена</label>
						<input id="passwordField" class="form-control" required="" type="number" v-model="offer.price" />
						<div class="invalid-feedback">
							Пожалуйста введите цену
						</div>
					</div>
				</div>

				<div class="col-sm-6">
					<div class="form-group">
						<label for="passwordField">Телефон</label>
						<input id="passwordField" class="form-control" required="" type="tel" v-model="offer.phone" />
						<div class="invalid-feedback">
							Пожалуйста введите телефон
						</div>
					</div>
				</div>
			</div>
		</form>
	</div>
</template>

<script lang="ts" src="./newOffer.ts"></script>