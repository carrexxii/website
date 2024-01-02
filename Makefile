all: js
	dotnet run

.PHONY: js
js:
	npm run babel
	cp ./node_modules/react/umd/react.production.min.js ./client/react.js
	cp ./node_modules/react-dom/umd/react-dom.production.min.js ./client/react-dom.js

.PHONY: watch
watch: js
	dotnet watch run

.PHONY: clean
clean:
	rm ./client/*.js
