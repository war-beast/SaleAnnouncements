<template>
	<div class="container-fluid">
		<div class="row">
			<div class="col-sm-4">
				<div class="d-flex flex-column">
					<h5 class="h5">Переписки ({{messageTitles.length}})</h5>
					<div class="border-bottom mb-2 pb-2" v-for="title in messageTitles">
						<span class="text-black-50"><a href="#" v-on:click="load(title.id)">{{title.name}}</a></span><br />
						<span class="text-muted">{{title.date}}</span>
					</div>
				</div>
			</div>

			<div class="col-sm-8 d-flex flex-column">
				<h5 class="h5">Переписка по объявлению <b>"{{messageThread.name}}"</b></h5>
				<div class="message-list" v-if="messageThread.messages.length > 0">
					<div v-for="message in messageThread.messages">
						<div class="alert alert-primary w-75 mb-2 ml-auto" role="alert" v-if="message.isMyMessage">
							<div>{{message.name}}, <span class="text-muted">{{message.date}}</span></div>
							<div>{{message.message}}</div>
						</div>
						<div class="alert alert-secondary w-75 mb-2 mr-auto" role="alert" v-else>
							<div>{{message.name}}, <span class="text-muted">{{message.date}}</span></div>
							<div>{{message.message}}</div>
						</div>
					</div>
				</div>
				<div class="message-list" v-else>
					Выберите ветку сообщений слева
				</div>

				<sendMessage v-bind:customerId="pageOptions.currentCustomerId"
							 v-bind:companionId="messageThread.companionId"
							 v-bind:parentMessageId="messageThread.id"
							 v-bind:addMessage = "(message) => addMessage(message)"
							 v-if="messageThread.id !== ''"/>
			</div>
		</div>
	</div>
</template>

<script src="./messages.ts"></script>