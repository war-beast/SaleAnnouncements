<template>
	<section>
		<div class="form-group">
			<label for="Message">Текст сообщения</label>
			<textarea class="form-control" v-model="message"></textarea>
			<span class="text-danger"></span>
			<span class="text-success"></span>
		</div>
		<div class="d-flex justify-content-end">
			<button type="button" class="btn btn-primary ml-auto" v-on:click="send">Отправить</button>
		</div>
	</section>
</template>

<script src="./sendMessage.ts"></script>