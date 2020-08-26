<template>
	<div class="row">
		<form v-bind:class="{ 'was-validated': !formValid }">
			<h4 class="h4">Добавить объявление <span class="text-muted">(все поля заполнить обязательно)</span></h4>
			<div class="text-danger" v-if="creationError != ''">{{creationError}}</div>

			<div class="form-group">
				<select class="form-control" v-model="selectedCategoryId">
					<option value="null" disabled selected>--Выберите категорию--</option>
					<option v-for="category in categories" v-bind:value="category.id">{{category.name}}</option>
				</select>
			</div>

			<div class="form-group">
				<label for="emailField">Название (заголовок объявления)</label>
				<input id="emailField" class="form-control" required="" v-model="offer.title" />
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

			<div class="custom-file" style="position: absolute; top: -500px">
				<input type="file" id="files" ref="files" v-on:change="handleFilesUpload" multiple />
				<label class="custom-file-label" for="files">Выберите фото</label>
			</div>
			<div class="d-flex flex-wrap flex-column">
				<div v-for="(photo, key) in photos" class="file-listing">
					{{ photo.name }}
					<button type="button" class="close" aria-label="Close" v-on:click="removeFile(key)" title="удалить">
						<span aria-hidden="true">&times;</span>
					</button>
				</div>
				<button v-on:click="addFiles()" class="btn btn-outline-secondary mt-3 w-50">Добавить фото</button>
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
						<input id="passwordField" class="form-control" required="" type="tel" v-model="offer.phoneNumber" />
						<div class="invalid-feedback">
							Пожалуйста введите телефон
						</div>
					</div>
				</div>
			</div>
			<div class="d-flex justify-content-between">
				<div>
					<span v-if="successMessage === ''">&nbsp</span>
					<div v-else class="alert alert-success" role="alert">
						{{successMessage}}
					</div>
				</div>
				<img src="/img/loader.gif" v-if="creationInProcess" />
				<button v-on:click="submit" class="btn btn-primary" type="button" role="button" v-else>
					<span>Сохранить</span>
				</button>
			</div>
		</form>
	</div>
</template>

<script lang="ts" src="./newOffer.ts"></script>