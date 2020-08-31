<template>
	<div class="container-fluid">
		<div class="row">
			<div class="col-sm-4">
				<div class="d-flex flex-column">
					<div class="border-bottom mb-2 pb-2" v-for="title in messageTitles">
						<span class="text-black-50"><a href="#" v-on:click="load(title.id)">{{title.name}}</a></span><br />
						<span class="text-muted">{{title.date}}</span>
					</div>
				</div>
			</div>

			<div class="col-sm-8 d-flex flex-column">
				<h4 class="h4">Переписка по объявлению <b>"{{messageHostOffer.name}}"</b></h4>
				<div class="message-list">
					<div class="alert alert-secondary w-75 mb-2 mr-auto" role="alert">
						<div>Вася, <span class="text-muted">31.08.2020</span></div>
						<div>Сообщение от Васи</div>
					</div>

					<div class="alert alert-primary w-75 mb-2 ml-auto" role="alert">
						<div>Я, <span class="text-muted">31.08.2020</span></div>
						<div>Мой ответ на сообщение выше</div>
					</div>
					<div class="alert alert-secondary w-75 mb-2 mr-auto" role="alert">
						<div>Вася, <span class="text-muted">31.08.2020</span></div>
						<div>Сообщение от Васи</div>
					</div>

					<div class="alert alert-primary w-75 mb-2 ml-auto" role="alert">
						<div>Я, <span class="text-muted">31.08.2020</span></div>
						<div>Мой ответ на сообщение выше</div>
					</div>
				</div>

				<div class="form-group">
					<label for="Message">Текст сообщения</label>
					<textarea id="Message" name="Message" class="form-control"></textarea>
					<span class="text-danger"></span>
					<span class="text-success"></span>
				</div>
				<div class="d-flex justify-content-end">
					<button type="button" class="btn btn-primary ml-auto">Отправить</button>
				</div>
			</div>
		</div>
	</div>
</template>

<script src="./messages.ts"></script>