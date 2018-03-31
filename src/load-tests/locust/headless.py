# Run load test with the following command:
# locust --host=http://localhost:3000 -f ./headless.py --no-web --clients 100 --hatch-rate 10 --num-request 1000
# --num-requests should in Locust 0.9 be replaced with --run-time

from locust import HttpLocust, TaskSet, task

class UserBehavior(TaskSet):
    @task
    def index(self):
        self.client.get("/")

    @task
    def profile(self):
        self.client.get("/profile")

class WebsiteUser(HttpLocust):
    task_set = UserBehavior