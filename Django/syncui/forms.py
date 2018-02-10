from django import forms

class datetimeForm(forms.Form):
	post = forms.DateTimeField()
	#'%Y-%m-%d %H-%M-%S'